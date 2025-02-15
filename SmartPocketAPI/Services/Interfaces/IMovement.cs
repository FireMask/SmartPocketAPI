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
    Task CreateMovementsFromRecurringPayment(Guid userId);
    Task<bool> CreateMovementBulkAsync(List<MovementViewModel> movementsViewModel);
    Task<object> GetDashboardInfoAsync(Guid userId);
}
