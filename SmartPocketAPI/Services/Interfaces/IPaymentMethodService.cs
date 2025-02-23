using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interface;

public interface IPaymentMethodService
{
    Task<List<PaymentMethod>> GetPaymentMethodsAsync(Guid userid);
    Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethodViewModel paymentMethodViewModel);
    Task<bool> DeletePaymentMethodAsync(Guid userid, int id);
    Task<PaymentMethod?> GetPaymentMethodByIdAsync(Guid userid, int id);
    Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethodViewModel paymentMethodVM);
}
