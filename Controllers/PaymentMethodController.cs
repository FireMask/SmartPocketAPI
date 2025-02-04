using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PaymentMethodController : Controller
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly ILogger<PaymentMethodController> _logger;

    public PaymentMethodController(IPaymentMethodService paymentMethodService, ILogger<PaymentMethodController> logger)
    {
        _paymentMethodService = paymentMethodService;
        _logger = logger;
    }

    [HttpGet("/PaymentMethods")]
    public async Task<IResult> GetPaymentMethods()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _paymentMethodService.GetPaymentMethodsAsync(userId);
            return Results.Json(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPaymentMethod(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _paymentMethodService.GetPaymentMethodByIdAsync(userId, id);
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> CreatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
    {
        try
        {
            if (paymentMethodViewModel == null)
                throw new ArgumentNullException(nameof(paymentMethodViewModel));

            paymentMethodViewModel.UserId = HttpContext.GetUserId();

            PaymentMethod newPaymentMethod = await _paymentMethodService.CreatePaymentMethodAsync(paymentMethodViewModel);

            return Results.Ok(newPaymentMethod);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpDelete]
    public async Task<IResult> DeleteCategory(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _paymentMethodService.DeletePaymentMethodAsync(userid, id);

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }
}
