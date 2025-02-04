using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IMovementTypeService
{
    Task<List<MovementType>> GetMovementTypesAsync();
    Task<MovementType?> CreateMovementTypeAsync(MovementTypeViewModel newMovement);
    Task<bool> DeleteMovementTypeAsync(int id);
    Task<MovementType?> GetMovementTypeByIdAsync(int id);
}
