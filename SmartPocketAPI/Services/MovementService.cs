
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.ApiResponse;
using SmartPocketAPI.Database;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.RequestModels;

namespace SmartPocketAPI.Services;

public class MovementService : IMovementService
{
    public ApplicationDbContext _context { get; }

    public MovementService(ApplicationDbContext context)
    {
        _context = context;
    }
    #region CRUD

    public async Task<PagedResult<Movement>> GetMovementsAsync(Guid id, MovementsRequest request)
    {
        var query = _context.Movements
            .Where(x => x.UserId == id)
            .Where(x => x.Amount > 0)
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .AsQueryable();

        if (request.CategoryId != null && request.CategoryId.Any())
            query = query.Where(x => request.CategoryId.Contains(x.CategoryId));

        if (request.PaymentMethodId != null && request.PaymentMethodId.Any())
            query = query.Where(x => request.PaymentMethodId.Contains(x.PaymentMethodId));

        if (request.MovementTypeId != null && request.MovementTypeId.Any())
            query = query.Where(x => request.MovementTypeId.Contains(x.MovementTypeId));

        if (request.StartDate.HasValue)
            query = query.Where(x => x.MovementDate >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(x => x.MovementDate <= request.EndDate.Value);

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => x.Description.Contains(request.Search));

        query = query.OrderByDescending(x => x.MovementDate)
            .ThenByDescending(x => x.CreatedAt);

        var result = await query.ToPagedListAsync(request.PageNumber, request.PageSize);

        return result;
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

    public async Task<Movement> UpdateMovementAsync(UpdateMovementViewModel updateMovement)
    {
        if (updateMovement == null)
            throw new ArgumentNullException(nameof(updateMovement));

        Movement movement = await _context.Movements.FirstAsync(x => x.Id == updateMovement.Id && x.UserId == updateMovement.userId);

        if (movement == null)
            throw new Exception("Movement does not exists");

        if (updateMovement.MovementDate.HasValue)
            movement.MovementDate = updateMovement.MovementDate.Value;

        if (updateMovement.Description != null)
            movement.Description = updateMovement.Description;

        if (updateMovement.Amount.HasValue)
            movement.Amount = updateMovement.Amount.Value;

        if (updateMovement.CategoryId.HasValue)
            movement.CategoryId = updateMovement.CategoryId.Value;

        if (updateMovement.PaymentMethodId.HasValue)
            movement.PaymentMethodId = updateMovement.PaymentMethodId.Value;

        if (updateMovement.MovementTypeId.HasValue)
            movement.MovementTypeId = updateMovement.MovementTypeId.Value;

        _context.Movements.Update(movement);

        await _context.SaveChangesAsync();

        return movement;
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

    #endregion

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
            .ThenByDescending(x => x.CreatedAt)
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

        var summaryPaymentMethods = await GetSummaryPaymentMethods(userId);

        Dictionary<string, object> result = new Dictionary<string, object>()
        {
            { "top20Movements", top20Movements },
            { "thisMonthMovementsCount", movementsCount },
            { "thisMonthSpent", thisMonthSpent },
            { "thisMonthIncome", thisMonthIncome },
            { "pendingMovementsRecurring", pendingMovementsRecurring.Count },
            { "summaryPaymentMethods", summaryPaymentMethods },
        };

        return result;
    }

    public async Task<PagedResult<RecurringPaymentsViewModel>> GetRecurringPaymentsWithPendingMovements(Guid userId, RecurringPaymentsRequest request)
    {
        var query = _context.RecurringPayments
            .Where(x => x.UserId == userId)
            .Include(x => x.Movements)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .Include(x => x.Frequency)
            .AsQueryable();

        if (request.PaymentMethodId != null && request.PaymentMethodId.Any())
        {
            query = query.Where(x => request.PaymentMethodId.Contains(x.PaymentMethodId));
        }
        if (request.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == request.CategoryId);
        }
        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }
        if (request.StartDate.HasValue)
        {
            query = query.Where(x => x.StartDate >= request.StartDate.Value);
        }
        if (request.EndDate.HasValue)
        {
            query = query.Where(x => x.StartDate <= request.EndDate.Value);
        }

        var recurringPayments = await query
            .OrderByDescending(x => x.IsActive)
            .ThenByDescending(x => x.StartDate)
            .ThenByDescending(x => x.PaymentMethod.Id)
            .ThenBy(x => x.Id)
            .Select(r => new RecurringPaymentsViewModel()
            {
                Id = r.Id,
                CategoryName = r.Category.Name,
                Description = r.Description,
                PaymentMethodName = r.PaymentMethod.Name,
                StartDate = r.StartDate,
                LastInstallmentDate = r.LastInstallmentDate,
                NextInstallmentDate = r.NextInstallmentDate,
                FrequencyName = r.Frequency.Name,
                InstallmentAmountPerPeriod = r.InstallmentAmountPerPeriod,
                MovementTypename = r.MovementType.Name,
                IsActive = r.IsActive,
                PaidCount = r.InstallmentCount == -1 ? $"{r.NextInstallmentCount - 1}" : $"{r.NextInstallmentCount - 1}/{r.InstallmentCount}",
                CanDelete = r.Movements.Count == 0
            })
            .ToListAsync();

        var pendingMovements = await GetPendingMovementsFromRecurringPayments(userId, request.PaymentMethodId, request.UntilDate);

        recurringPayments.ForEach(x =>
        {
            x.PendingMovements = pendingMovements
                .Where(y => y.RecurringPaymentId == x.Id)
                .ToList();
        });

        if (request.HasPendingMovements)
            recurringPayments = recurringPayments.Where(x => x.PendingMovements.Count > 0).ToList();

        return recurringPayments.ToPagedListAsync(request.PageNumber, request.PageSize);
    }

    public async Task<List<MovementFromRecurringPaymentsViewModel>> GetPendingMovementsFromRecurringPayments(Guid userId, int[]? _paymentMethodId = null, DateOnly? untilDate = null)
    {
        var query = _context.RecurringPayments
            .Where(x => x.UserId == userId)
            .Include(x => x.Movements)
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.MovementType)
            .Include(x => x.CreditCardPayment)
            .Include(x => x.Frequency)
            .AsQueryable();

        if (_paymentMethodId != null && _paymentMethodId.Any())
        {
            query = query.Where(x => _paymentMethodId.Contains(x.PaymentMethodId));
        }

        List<RecurringPayment> recurringPayments = await query.ToListAsync();

        if (untilDate == null)
            untilDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        var addMovementList = new List<MovementFromRecurringPaymentsViewModel>();

        if (recurringPayments == null)
            return addMovementList;

        foreach (var rPayment in recurringPayments)
        {
            DateOnly nextDate = rPayment.NextInstallmentDate;
            int installmentNo = rPayment.NextInstallmentCount;

            if (!rPayment.IsActive)
                continue;

            while (nextDate <= untilDate)
            {
                if (rPayment.InstallmentCount != -1 //Si la el pago recurrente es indefinido
                    && rPayment.InstallmentCount < installmentNo //Si el numero de cuotas ya se cumplio
                    )
                    break;

                addMovementList.Add(new MovementFromRecurringPaymentsViewModel()
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
                    FrequencyId = rPayment.FrequencyId,
                    FrequencyName = rPayment.Frequency.Name
                });
                nextDate = RecurringPaymentHelper.GetNextDate(nextDate, rPayment.FrequencyId);
            }
        }

        return addMovementList
            .OrderByDescending(x => x.MovementDate)
            .ToList();
    }

    public async Task<object> GetSummaryPaymentMethods(Guid userId)
    {
        var cards = await _context.PaymentMethods
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.IsCreditCard)
            .Include(x => x.Movements)
            .Include(x => x.RecurringPayments)
            .ToListAsync();

        var cardsMonthSummary = new List<CardMonthSummaryViewModel>();

        foreach (var card in cards)
        {
            var now = DateTime.UtcNow;
            DateOnly startTransactionDate = new DateOnly(now.Year, now.Month, card.TransactionDate);
            DateOnly endTransactionDate;

            if (card.TransactionDate > now.Day) //La fecha de corte ya paso
                startTransactionDate = startTransactionDate.AddMonths(-1); //Tomar la fecha de corte anterior

            endTransactionDate = startTransactionDate.AddMonths(1);

            DateOnly dueDate = new DateOnly(endTransactionDate.Year, endTransactionDate.Month, card.DueDate);

            if (card.DueDate < card.TransactionDate)
                dueDate = dueDate.AddMonths(1);

            var cardMovements = card.Movements
                .Where(x => x.MovementDate > startTransactionDate && x.MovementDate <= endTransactionDate)
                .ToList();

            var pendingMovements = await GetPendingMovementsFromRecurringPayments(userId, [card.Id], endTransactionDate);

            //PendingMovements
            var pendingMovementsRange = pendingMovements
                .Where(x => x.MovementDate > startTransactionDate && x.MovementDate <= endTransactionDate)
                .Sum(x => x.Amount);

            //Movements already captured
            var thisPeriodSpent = cardMovements
                .Where(x => x.MovementTypeId != 2)
                .Sum(x => x.Amount);

            cardsMonthSummary.Add(new CardMonthSummaryViewModel
            {
                CardName = card.Name,
                TotalSum = thisPeriodSpent + pendingMovementsRange,
                ThisPeriodAmount = thisPeriodSpent,
                PendingMovementsAmount = pendingMovementsRange,
                TransactionStartDate = startTransactionDate,
                TransactionEndDate = endTransactionDate,
                DueDate = dueDate
            });
        }

        return cardsMonthSummary;
    }

    public async Task<object> GetSummaryPaymentMethodsPerPeriod(Guid userId, PaymentMethodsProjectionRequest request)
    {
        var cards = await _context.PaymentMethods
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Where(x => x.IsCreditCard)
            .Include(x => x.Movements)
                .ThenInclude(m => m.Category)
            .Include(x => x.RecurringPayments)
            .ToListAsync();

        var pendingMovements = await GetPendingMovementsFromRecurringPayments(userId, null, GetPeriodDate(1, 7));

        var cardSummaries = cards
            .Select(card => new
            {
                card.Name,
                Periods = Enumerable.Range(request.MonthsBefore, request.MonthsAhead - request.MonthsBefore)
                    .Select(i =>
                    {
                        var startDate = GetPeriodDate(card.TransactionDate, i).AddDays(1);
                        var endDate = GetPeriodDate(card.TransactionDate, i + 1);

                        int lastDayOfMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
                        int tempDueDate = card.DueDate < lastDayOfMonth ? card.DueDate : lastDayOfMonth;
                        DateOnly dueDate = new DateOnly(endDate.Year, endDate.Month, tempDueDate);
                        if (card.DueDate < card.TransactionDate)
                            dueDate = dueDate.AddMonths(1);

                        List<MovementRecord> movements = new List<MovementRecord>();

                        movements.AddRange(card.Movements
                            .Where(x => x.MovementDate >= startDate && x.MovementDate <= endDate)
                            .Select(m => new MovementRecord(
                                m.Description,
                                false,
                                m.Category.Name,
                                m.Amount,
                                m.MovementDate
                            ))
                            .ToList()
                        );

                        movements.AddRange(pendingMovements
                            .Where(x => x.MovementDate >= startDate && x.MovementDate <= endDate)
                            .Where(x => x.PaymentMethodId == card.Id)
                            .Select(pm => new MovementRecord(
                                pm.Description,
                                true,
                                pm.CategoryName,
                                pm.Amount,
                                pm.MovementDate
                            ))
                            .ToList()
                        );

                        return new
                        {
                            StartDate = startDate,
                            EndDate = endDate,
                            DueDate = dueDate,
                            Movements = movements.OrderByDescending(x => x.MovementDate),
                            Amount = movements.Sum(x => x.Amount)
                        };
                    }).ToList()
            }).ToList();

        return cardSummaries;
    }

    public async Task<object> GetSummaryMovementsPerMonth(Guid userId, DateOnly startMonth, DateOnly endMonth)
    {
        var cards = await _context.PaymentMethods
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Where(x => x.IsCreditCard)
            .Include(x => x.Movements)
            .Include(x => x.RecurringPayments)
            .ToListAsync();

        var pendingMovements = await GetPendingMovementsFromRecurringPayments(userId, null, endMonth);

        var cardSummaries = cards
            .Select(card =>
            {
                List<MovementRecord> movements = new List<MovementRecord>();

                movements.AddRange(card.Movements
                    .Select(m => new MovementRecord(
                        m.Description,
                        false,
                        m.Category.Name,
                        m.Amount,
                        m.MovementDate
                    ))
                    .ToList()
                );

                movements.AddRange(pendingMovements
                    .Where(x => x.PaymentMethodId == card.Id)
                    .Select(pm => new MovementRecord(
                        pm.Description,
                        true,
                        pm.CategoryName,
                        pm.Amount,
                        pm.MovementDate
                    ))
                    .ToList()
                );

                var movementsPerMonth = movements
                    .GroupBy(m => new { m.MovementDate.Year, m.MovementDate.Month })
                    .Select(m => new
                    {
                        Date = new DateOnly(m.Key.Year, m.Key.Month, 1),
                        Amount = m.Sum(x => x.Amount),
                        Movements = m.ToList(),
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return new
                {
                    Card = card.Name,
                    Movements = movementsPerMonth
                };
            }).ToList();

        return cardSummaries;
    }

    private DateOnly GetPeriodDate(int transactionDate, int monthOffset)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        // Calculate the transaction date for the current month
        var thisMonthTransaction = new DateOnly(today.Year, today.Month, transactionDate);

        // If today is before this month's transaction date, use the last month's date
        if (today < thisMonthTransaction)
        {
            thisMonthTransaction = thisMonthTransaction.AddMonths(-2);
        }

        return thisMonthTransaction.AddMonths(monthOffset);
    }

    public async Task<object> GetCatalogs(Guid userId)
    {
        var categories = await _context.Categories
            .Where(x => x.UserId == userId || x.IsDefault)
            .Select(o => new
            {
                o.Id,
                o.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        var frequencies = await _context.Frequency
            .Select(o => new
            {
                o.Id,
                o.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        var paymentMethods = await _context.PaymentMethods
            .Where(x => x.UserId == userId || x.IsDefault)
            .Select(o => new
            {
                o.Id,
                o.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        var movementTypes = await _context.MovementType
            .Select(o => new
            {
                o.Id,
                o.Name
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        return new Dictionary<string, object>()
        {
            { "categories", categories },
            { "frequencies", frequencies },
            { "paymentMethods", paymentMethods },
            { "movementTypes", movementTypes }
        }; ;
    }

    public async Task<List<Movement>> LoadMovmentsFromFileAsync(Guid userId, IFormFile fileInfo)
    {
        using var ms = new MemoryStream();
        await fileInfo.CopyToAsync(ms);
        ms.Position = 0;

        List<MovementDocument> movements = DocumentHelper.LoadFromXLSFile(ms);

        if (movements == null || !movements.Any())
            throw new Exception("No movements found in the file");

        List<Movement> movementList = new List<Movement>();
        
        List<PaymentMethod> paymentMethods = await _context.PaymentMethods
            .Where(x => x.UserId == userId)
            .ToListAsync();

        List<Movement> existingMovements = await _context.Movements
            .Where(x => x.UserId == userId)
            .ToListAsync();

        foreach (var doc in movements)
        {
            PaymentMethod? pm = paymentMethods.FirstOrDefault(x => x.Name == doc.Card && x.UserId == userId && x.IsActive);
            // If the payment method does not exist, skip this document
            if (pm == null)
                continue;
            
            // If a movement with the same date, description, and amount already exists, skip this document
            if (existingMovements.Any(x => x.UserId == userId && x.MovementDate == doc.Date && x.Description == doc.Concept && x.Amount == doc.Amount))
                continue;

            movementList.Add(new Movement
            {
                MovementDate = doc.Date,
                Description = doc.Concept,
                Amount = doc.Amount,
                UserId = userId,
                CategoryId = 6, //Others
                PaymentMethodId = pm.Id,
                MovementTypeId = 3, // Purchase
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        _context.Movements.AddRange(movementList);
        await _context.SaveChangesAsync();

        return movementList;
    }
}