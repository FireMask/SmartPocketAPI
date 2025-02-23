using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
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

    #region CRUD

    [HttpGet("/PaymentMethods")]
    public async Task<IResult> GetPaymentMethods()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _paymentMethodService.GetPaymentMethodsAsync(userId);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPaymentMethod(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _paymentMethodService.GetPaymentMethodByIdAsync(userId, id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
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

            return newPaymentMethod.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePaymentMethod(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _paymentMethodService.DeletePaymentMethodAsync(userid, id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }

    [HttpPut]
    public async Task<IResult> UpdatePaymentMethod(PaymentMethodViewModel paymentMethodViewModel)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            paymentMethodViewModel.UserId = userId;
            var result = await _paymentMethodService.UpdatePaymentMethodAsync(paymentMethodViewModel);
            return result.ToApiResponse(Constants.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_ERROR);
        }
    }

    #endregion
}
