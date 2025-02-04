using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;

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
    public async Task<IResult> GetRecurringPayments()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _recurringPaymentService.GetRecurringPaymentsAsync(userId);
            return Results.Json(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpGet]
    public async Task<IResult> GetRecurringPaymentById(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var recurringPayment = await _recurringPaymentService.GetRecurringPaymentByIdAsync(userId, id);
            return Results.Ok(recurringPayment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> CreateRecurrinPayment(RecurringPaymentViewModel recurringPayment)
    {
        try
        {
            if (recurringPayment == null)
                throw new ArgumentNullException(nameof(recurringPayment));

            recurringPayment.UserId = HttpContext.GetUserId();

            var newRecurringPayment = await _recurringPaymentService.CreateRecurringPaymentAsync(recurringPayment);

            return Results.Ok(newRecurringPayment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpDelete]
    public async Task<IResult> DeleteRecurringPayment(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _recurringPaymentService.DeleteRecurringPaymentAsync(userid, id);

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpPut]
    public async Task<IResult> UpdateRecurringPayment(RecurringPaymentViewModel recurringPayment)
    {
        try
        {
            recurringPayment.UserId = HttpContext.GetUserId();
            var result = await _recurringPaymentService.UpdateRecurringPaymentAsync(recurringPayment);

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }
}
