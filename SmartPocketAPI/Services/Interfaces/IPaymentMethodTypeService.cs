using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IPaymentMethodTypeService
{
    Task<List<PaymentMethodType>> GetPaymentMethodTypesAsync();
    Task<PaymentMethodType> CreatePaymentMethodTypeAsync(PaymentMethodTypeViewModel newPaymentMethod);
    Task<bool> DeletePaymentMethodTypeAsync(int id);
    Task<PaymentMethodType?> GetPaymentMethodTypeByIdAsync(int id);
}
