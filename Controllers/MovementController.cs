using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;
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
    public async Task<IResult> GetMovementTypes()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetMovementsAsync(userId);
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
                InstallmentNumber = 1,
                MovementDate = movementViewModel.MovementDate,
                Description = movementViewModel.Description,
                Amount = movementViewModel.Amount,
                CategoryId = movementViewModel.CategoryId,
                PaymentMethodId = movementViewModel.PaymentMethodId,
                MovementTypeId = movementViewModel.MovementTypeId,
                CreditCardPaymentId = movementViewModel.CreditCardPaymentId
            };

            RecurringPaymentViewModel recurringPayment = new RecurringPaymentViewModel()
            {
                Description = movementViewModel.Description,
                IsInterestFreePayment = movementViewModel.IsInterestFreePayment,
                InstallmentCount = movementViewModel.InstallmentCount <= 0 ? 1 : movementViewModel.InstallmentCount,
                InstallmentAmount = movementViewModel.Amount,
                StartDate = movementViewModel.MovementDate,
                UserId = userId,
                FrecuencyId = movementViewModel.FrecuencyId
            };

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (movementViewModel.IsInstallment)
            {
                var rpResult = await _recurringPaymentService.CreateRecurringPaymentAsync(recurringPayment);
                movement.RecurringPaymentId = rpResult.Id;
                movement.Amount = movementViewModel.Amount / rpResult.InstallmentCount;
            }

            var mResult = await _movementService.CreateMovementAsync(movement);

            scope.Complete();

            return mResult.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            if(ex.InnerException != null)
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
}
