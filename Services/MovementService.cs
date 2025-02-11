
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services;

public class MovementService : IMovementService
{
    public ApplicationDbContext _context { get; }

    public MovementService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movement>> GetMovementsAsync(Guid id)
    {
        return await _context.Movements
            .Where(x => x.UserId == id)
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .ToListAsync();
    }

    public async Task<Movement?> CreateMovementAsync(MovementViewModel movementViewModel)
    {
        Movement newMovement = new Movement
        {
            MovementDate = movementViewModel.MovementDate,
            Description = movementViewModel.Description,
            Amount = movementViewModel.Amount,
            UserId = movementViewModel.UserId,
            CategoriId = movementViewModel.CategoryId,
            PaymentMethodId = movementViewModel.PaymentMethodId,
            RecurringPaymentId = movementViewModel.RecurringPaymentId,
            MovementTypeId = movementViewModel.MovementTypeId,
            CreditCardPaymentId = movementViewModel.CreditCardPaymentId,
        };

        _context.Movements.Add(newMovement);

        await _context.SaveChangesAsync();

        return newMovement;
    }

    public async Task<bool> DeleteMovementAsync(Guid userId, int id)
    {
        Movement? movement = await GetMovementByIdAsync(userId, id);
        if (movement == null)
            throw new Exception("Movement does not exists");

        _context.Movements.Remove(movement);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Movement?> GetMovementByIdAsync(Guid userId, int movementId)
    {
        Movement? movement = await _context.Movements
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .FirstOrDefaultAsync(x => x.Id == movementId && x.UserId == userId);
        return movement;
    }
}