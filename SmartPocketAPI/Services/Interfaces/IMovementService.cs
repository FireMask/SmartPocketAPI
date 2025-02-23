using SmartPocketAPI.ApiResponse;
using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.RequestModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IMovementService
{
    Task<PagedResult<Movement>> GetMovementsAsync(Guid id, MovementsRequest request);
    Task<Movement> CreateMovementAsync(MovementViewModel newMovement);
    Task<bool> DeleteMovementAsync(Guid userId, int id);
    Task<Movement> GetMovementByIdAsync(Guid userId, int movementId);
    Task<Movement> UpdateMovementAsync(UpdateMovementViewModel updateMovement);
    Task<List<MovementFromRecurringPaymentsViewModel>> GetPendingMovementsFromRecurringPayments(Guid userId, int? paymentMethodId = null, DateOnly? untilDate = null);
    Task<object> GetDashboardInfoAsync(Guid userId);
    Task<object> GetSummaryPaymentMethods(Guid userId);
}
