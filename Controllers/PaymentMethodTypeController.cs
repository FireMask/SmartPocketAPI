using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PaymentMethodTypeController : Controller
{
    private readonly IPaymentMethodTypeService _paymentMethodTypeService;
    private readonly ILogger<PaymentMethodTypeController> _logger;

    public PaymentMethodTypeController(IPaymentMethodTypeService paymentMethodTypeService, ILogger<PaymentMethodTypeController> logger)
    {
        _paymentMethodTypeService = paymentMethodTypeService;
        _logger = logger;
    }

    [HttpGet("/PaymentMethodTypes")]
    public async Task<IResult> GetPaymentMethodTypes()
    {
        try
        {
            var result = await _paymentMethodTypeService.GetPaymentMethodTypesAsync();
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPaymentMethodType(int id)
    {
        try
        {
            var result = await _paymentMethodTypeService.GetPaymentMethodTypeByIdAsync(id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreatePaymentMethod(PaymentMethodTypeViewModel paymentMethodTypeViewModel)
    {
        try
        {
            if (paymentMethodTypeViewModel == null)
                throw new ArgumentNullException(nameof(paymentMethodTypeViewModel));

            PaymentMethodType newPaymentMethod = await _paymentMethodTypeService.CreatePaymentMethodTypeAsync(paymentMethodTypeViewModel);

            return newPaymentMethod.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteCategory(int id)
    {
        try
        {
            var result = await _paymentMethodTypeService.DeletePaymentMethodTypeAsync(id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
