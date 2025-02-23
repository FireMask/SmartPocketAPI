using SmartPocketAPI.ApiResponse;
using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.RequestModels;

namespace SmartPocketAPI.Services.Interface;

public interface IRecurringPaymentService
{
    Task<PagedResult<RecurringPaymentsViewModel>> GetRecurringPaymentsAsync(Guid userid, RecurringPaymentsRequest request);
    Task<RecurringPayment> CreateRecurringPaymentAsync(RecurringPaymentViewModel recurringPaymentViewModel);
    Task<bool> DeleteRecurringPaymentAsync(Guid userid, int id);
    Task<RecurringPayment> UpdateRecurringPaymentAsync(RecurringPaymentViewModel recurringPaymentViewModel);
    Task<RecurringPayment?> GetRecurringPaymentByIdAsync(Guid userid, int id);
    Task UpdateRecurringPaymentNewMovementAsync(UpdateRecurringPaymentViewModel updateRP);
}
