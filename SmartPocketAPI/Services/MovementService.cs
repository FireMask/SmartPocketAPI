
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Helpers;
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

    public async Task<Movement> CreateMovementAsync(MovementViewModel movementViewModel)
    {
        Movement newMovement = new Movement
        {
            MovementDate = movementViewModel.MovementDate,
            Description = movementViewModel.Description,
            Amount = movementViewModel.Amount,
            UserId = movementViewModel.UserId,
            CategoryId = movementViewModel.CategoryId,
            PaymentMethodId = movementViewModel.PaymentMethodId,
            MovementTypeId = movementViewModel.MovementTypeId,
            CreditCardPaymentId = movementViewModel.CreditCardPaymentId,
            RecurringPaymentId = movementViewModel.RecurringPaymentId,
            InstallmentNumber = movementViewModel.InstallmentNumber
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

    public async Task<Movement> GetMovementByIdAsync(Guid userId, int movementId)
    {
        Movement movement = await _context.Movements
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .FirstAsync(x => x.Id == movementId && x.UserId == userId);

        if (movement == null)
            throw new Exception("Movement does not exists");

        return movement;
    }

    public async Task<Movement> UpdateMovementAsync(UpdateMovementViewModel updateMovement)
    {
        if (updateMovement == null)
            throw new ArgumentNullException(nameof(updateMovement));

        Movement movement = await _context.Movements.FirstAsync(x => x.Id == updateMovement.Id && x.UserId == updateMovement.userId);

        if (movement == null)
            throw new Exception("Movement does not exists");

        if(updateMovement.MovementDate != null)
            movement.MovementDate = (DateTime)updateMovement.MovementDate;
        if(updateMovement.Description != null)
            movement.Description = updateMovement.Description;
        if(updateMovement.Amount != null)
            movement.Amount = (decimal)updateMovement.Amount;
        if(updateMovement.CategoryId != null)
            movement.CategoryId = (int)updateMovement.CategoryId;
        if(updateMovement.PaymentMethodId != null)
            movement.PaymentMethodId = (int)updateMovement.PaymentMethodId;
        if(updateMovement.MovementTypeId != null)
            movement.MovementTypeId = (int)updateMovement.MovementTypeId;

        _context.Movements.Update(movement);

        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<object> GetDashboardInfoAsync(Guid userId)
    {
        var query = _context.Movements
            .Where(x => x.UserId == userId)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .OrderByDescending(x => x.MovementDate)
            .AsQueryable();

        var movementThisMonth = await query.Where(x => x.MovementDate.Year == DateTime.Now.Year
                && x.MovementDate.Month == DateTime.Now.Month)
            .ToListAsync();

        var top20Movements = await query.Take(20).ToListAsync();
        var movementsCount = movementThisMonth.Count();

        //Sum of amount of not income (spent)
        var thisMonthSpent = movementThisMonth
            .Where(x => x.MovementTypeId != 2)
            .Sum(x => x.Amount);

        //Sum of amount of income this month
        var thisMonthIncome = movementThisMonth
            .Where(x => x.MovementTypeId == 2)
            .Sum(x => x.Amount);

        var pendingMovementsRecurring = await GetPendingMovementsFromRecurringPayments(userId);

        Dictionary<string, object> result = new Dictionary<string, object>()
        {
            { "top20Movements", movementThisMonth },
            { "thisMonthMovementsCount", movementsCount },
            { "thisMonthSpent", thisMonthSpent },
            { "thisMonthIncome", thisMonthIncome },
            { "pendingMovementsRecurring", pendingMovementsRecurring.Count },
        };

        return result;
    }

    public async Task<List<object>> GetPendingMovementsFromRecurringPayments(Guid userId)
    {
        List<RecurringPayment> recurringPayments = await _context.RecurringPayments
            .Where(x => x.UserId == userId)
            .Include(x => x.Movements)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .ToListAsync();

        var addMovementList = new List<object>();

        if (recurringPayments == null)
            return addMovementList;

        foreach (var rPayment in recurringPayments)
        {
            DateOnly nextDate = rPayment.NextInstallmentDate;
            int installmentNo = rPayment.NextInstallmentCount;

            if (!rPayment.IsActive)
                continue;

            while (nextDate <= DateOnly.FromDateTime(DateTime.UtcNow.Date))
            {
                addMovementList.Add(new
                {
                    MovementDate = nextDate,
                    Description = rPayment.Description,
                    Amount = rPayment.InstallmentAmountPerPeriod,
                    CategoryId = rPayment.CategoryId,
                    CategoryName = rPayment.Category.Name,
                    PaymentMethodId = rPayment.PaymentMethodId,
                    PaymentMethodName = rPayment.PaymentMethod.Name,
                    RecurringPaymentId = rPayment.Id,
                    MovementTypeId = rPayment.MovementTypeId,
                    MovementTypeName = rPayment.MovementType.Name,
                    InstallmentCount = rPayment.InstallmentCount,
                    InstallmentNumber = installmentNo++,
                    CreditCardPaymentId = rPayment.CreditCardPaymentId,
                    CreditCardPaymentName = rPayment.CreditCardPayment?.Name ?? "",
                });
                nextDate = RecurringPaymentHelper.GetNextDate(nextDate, rPayment.FrecuencyId);            
            }
        }

        return addMovementList;
    }
}