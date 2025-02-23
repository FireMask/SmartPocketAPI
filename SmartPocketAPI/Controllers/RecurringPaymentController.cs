using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.ViewModels.RequestModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class RecurringPaymentController : Controller
{
    private readonly IRecurringPaymentService _recurringPaymentService;
    private readonly ILogger<RecurringPaymentController> _logger;

    public RecurringPaymentController(IRecurringPaymentService recurringPayment, ILogger<RecurringPaymentController> logger)
    {
        _recurringPaymentService = recurringPayment;
        _logger = logger;
    }

    [HttpGet("/RecurringPayments")]
    public async Task<IResult> GetRecurringPayments([FromBody] RecurringPaymentsRequest request)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _recurringPaymentService.GetRecurringPaymentsAsync(userId, request);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet]
    public async Task<IResult> GetRecurringPaymentById(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var recurringPayment = await _recurringPaymentService.GetRecurringPaymentByIdAsync(userId, id);
            return recurringPayment.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateRecurringPayment(RecurringPaymentViewModel recurringPayment)
    {
        try
        {
            if (recurringPayment == null)
                throw new ArgumentNullException(nameof(recurringPayment));

            recurringPayment.UserId = HttpContext.GetUserId();

            var newRecurringPayment = await _recurringPaymentService.CreateRecurringPaymentAsync(recurringPayment);

            return newRecurringPayment.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteRecurringPayment(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _recurringPaymentService.DeleteRecurringPaymentAsync(userid, id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdateRecurringPayment(RecurringPaymentViewModel recurringPayment)
    {
        try
        {
            recurringPayment.UserId = HttpContext.GetUserId();
            var result = await _recurringPaymentService.UpdateRecurringPaymentAsync(recurringPayment);
            return result.ToApiResponse(Constants.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_ERROR);
        }
    }
}
