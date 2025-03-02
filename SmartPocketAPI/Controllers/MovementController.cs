using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.RequestModels;
using System.Transactions;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MovementController : Controller
{
    private readonly IMovementService _movementService;
    private readonly IRecurringPaymentService _recurringPaymentService;
    private readonly ILogger<MovementController> _logger;

    public MovementController(IMovementService movementService, IRecurringPaymentService recurringPaymentService, ILogger<MovementController> logger)
    {
        _movementService = movementService;
        _recurringPaymentService = recurringPaymentService;
        _logger = logger;
    }

    [HttpGet("/Movements")]
    public async Task<IResult> GetMovementTypes([FromQuery] MovementsRequest request)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetMovementsAsync(userId, request);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetMovement(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetMovementByIdAsync(userId, id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateMovement(MovementMSIViewModel movementViewModel)
    {
        try
        {
            if (movementViewModel == null)
                throw new ArgumentNullException(nameof(movementViewModel));

            Guid userId = HttpContext.GetUserId();

            MovementViewModel movement = new MovementViewModel()
            {
                UserId = userId,
                MovementDate = movementViewModel.MovementDate,
                Description = movementViewModel.Description,
                Amount = movementViewModel.Amount,
                CategoryId = movementViewModel.CategoryId,
                PaymentMethodId = movementViewModel.PaymentMethodId,
                MovementTypeId = movementViewModel.MovementTypeId,
                CreditCardPaymentId = movementViewModel.CreditCardPaymentId,
                InstallmentNumber = movementViewModel.InstallmentNumber
            };

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            if (movementViewModel.IsInstallment)
            {
                RecurringPaymentViewModel recurringPayment = new RecurringPaymentViewModel()
                {
                    Description = movementViewModel.Description,
                    IsInterestFreePayment = movementViewModel.IsInterestFreePayment,
                    InstallmentCount = movementViewModel.InstallmentCount <= 0 ? 1 : movementViewModel.InstallmentCount,
                    LastInstallmentDate = movementViewModel.MovementDate,
                    NextInstallmentDate = RecurringPaymentHelper.GetNextDate(movementViewModel.MovementDate, movementViewModel.FrequencyId),
                    NextInstallmentCount = 2, //The first movement will be created, so the next will be the 2nd
                    InstallmentAmount = movementViewModel.Amount,
                    StartDate = movementViewModel.MovementDate,
                    IsActive = true,
                    CategoryId = movementViewModel.CategoryId,
                    PaymentMethodId = movementViewModel.PaymentMethodId,
                    MovementTypeId = movementViewModel.MovementTypeId,
                    FrequencyId = movementViewModel.FrequencyId,
                    UserId = userId,
                };

                var rpResult = await _recurringPaymentService.CreateRecurringPaymentAsync(recurringPayment);

                movement.RecurringPaymentId = rpResult.Id;
                movement.InstallmentNumber = 1; //Its the first movement of the recurringPayment
                movement.Amount = movementViewModel.Amount / rpResult.InstallmentCount;
            }
            var mResult = await _movementService.CreateMovementAsync(movement);

            scope.Complete();
            return mResult.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            if (ex.InnerException != null)
                return ex.InnerException.Message.ToApiError(Constants.INSERT_ERROR);
            else
                return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteMovement(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.DeleteMovementAsync(userId, id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdateMovement(UpdateMovementViewModel updateMovementViewModel)
    {
        try
        {
            if (updateMovementViewModel == null)
                throw new ArgumentNullException(nameof(updateMovementViewModel));

            updateMovementViewModel.userId = HttpContext.GetUserId();
            var result = await _movementService.UpdateMovementAsync(updateMovementViewModel);

            return result.ToApiResponse(Constants.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_ERROR);
        }
    }

    [HttpGet("/Dashboard")]
    public async Task<IResult> GetDashboardInfo()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetDashboardInfoAsync(userId);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("/PendingMovements")]
    public async Task<IResult> PendingMovements([FromQuery] RecurringPaymentsRequest request)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetRecurringPaymentsWithPendingMovements(userId, request);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost("/CreateNewMovementFromRecurringPayment")]
    public async Task<IResult> CreateNewMovementFromRecurringPayment(MovementMSIViewModel movementMSIViewModel)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            
            int recurringPaymentId = movementMSIViewModel.RecurringPaymentId ?? 0;

            var recurringPayment = await _recurringPaymentService.GetRecurringPaymentByIdAsync(userId, recurringPaymentId);
            if (recurringPayment == null)
                throw new Exception("Recurring payment does not exists");

            MovementViewModel movement = new MovementViewModel()
            {
                MovementDate = movementMSIViewModel.MovementDate,
                Description = movementMSIViewModel.Description,
                Amount = movementMSIViewModel.Amount,
                UserId = userId,
                CategoryId = movementMSIViewModel.CategoryId,
                PaymentMethodId = movementMSIViewModel.PaymentMethodId,
                RecurringPaymentId = movementMSIViewModel.RecurringPaymentId,
                MovementTypeId = movementMSIViewModel.MovementTypeId,
                InstallmentNumber = movementMSIViewModel.InstallmentNumber,
                CreditCardPaymentId = movementMSIViewModel.CreditCardPaymentId
            };

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var newMovement = await _movementService.CreateMovementAsync(movement);

            await _recurringPaymentService.UpdateRecurringPaymentNewMovementAsync(new UpdateRecurringPaymentViewModel()
            {
                UserId = userId,
                Id = recurringPaymentId,
                NextInstallmentDate = RecurringPaymentHelper.GetNextDate(movementMSIViewModel.MovementDate, recurringPayment.FrequencyId),
                NextInstallmentCount = recurringPayment.NextInstallmentCount + 1,
                LastInstallmentDate = movementMSIViewModel.MovementDate
            });

            scope.Complete();

            return newMovement.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpGet("/FilterCatalogs")]
    public async Task<IResult> FilterCatalogs()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetCatalogs(userId);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("/PaymentMethodsProjection/{monthsAhead}")]
    public async Task<IResult> PaymentMethodsProjection(int monthsAhead = 6)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetSummaryPaymentMethodsPerPeriod(userId, monthsAhead);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("/test")]
    public async Task<IResult> Test()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetSummaryPaymentMethodsPerPeriod(userId, 6);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }
}
