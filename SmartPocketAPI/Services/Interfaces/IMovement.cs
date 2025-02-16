using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IMovementService
{
    Task<List<Movement>> GetMovementsAsync(Guid id);
    Task<Movement> CreateMovementAsync(MovementViewModel newMovement);
    Task<bool> DeleteMovementAsync(Guid userId, int id);
    Task<Movement> GetMovementByIdAsync(Guid userId, int movementId);
    Task<Movement> UpdateMovementAsync(UpdateMovementViewModel updateMovement);
    Task<List<MovementFromRecurringPaymentsViewModel>> GetPendingMovementsFromRecurringPayments(Guid userId, int? paymentMethodId = null, DateOnly? untilDate = null);
    Task<object> GetDashboardInfoAsync(Guid userId);
    Task<object> GetSummaryPaymentMethods(Guid userId);
}
