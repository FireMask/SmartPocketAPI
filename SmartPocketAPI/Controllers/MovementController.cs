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
    public async Task<IResult> CreateMovement(MovementViewModel movementViewModel)
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

            var mResult = await _movementService.CreateMovementAsync(movement);

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

    [HttpGet("/test")]
    public async Task<IResult> Test()
    {
        Guid userId = HttpContext.GetUserId();
        //await _movementService.CreateMovementsFromRecurringPayment(userId);
        return Results.Ok();
    }
}
