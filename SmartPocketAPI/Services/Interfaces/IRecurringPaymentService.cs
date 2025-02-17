using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interface;

public interface IRecurringPaymentService
{
    Task<object> GetRecurringPaymentsAsync(Guid userid);
    Task<RecurringPayment> CreateRecurringPaymentAsync(RecurringPaymentViewModel recurringPaymentViewModel);
    Task<bool> DeleteRecurringPaymentAsync(Guid userid, int id);
    Task<RecurringPayment> UpdateRecurringPaymentAsync(RecurringPaymentViewModel recurringPaymentViewModel);
    Task<RecurringPayment?> GetRecurringPaymentByIdAsync(Guid userid, int id);
}
