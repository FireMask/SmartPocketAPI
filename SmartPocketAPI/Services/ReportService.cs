using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.Filters;

namespace SmartPocketAPI.Services.Interfaces;

public class ReportService : IReportService
{
    public ApplicationDbContext _context { get; }

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovementGeneralReport> GetMonthlyReport(MovementFilter filter)
    {
        var data = await _context.Movements
            .Include(x => x.Category)
            .Include(x => x.PaymentMethod)
            .Include(x => x.RecurringPayment)
            .Where(x => x.UserId == filter.UserId)
            .Where(x => x.MovementDate >= filter.StartDate && x.MovementDate <= filter.EndDate)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var resultList = data
            .Select(x =>
            {
                var instCount = "";
                if(x.RecurringPayment != null)
                {
                    instCount = x.RecurringPayment?.InstallmentCount == -1 ? $"{x.InstallmentNumber}" : $"{x.InstallmentNumber}/{x.RecurringPayment?.InstallmentCount}";
                }

                return new MovementReportEntity
                {
                    MovementDate = x.MovementDate,
                    CategoryName = x.Category.Name,
                    PaymentMethodName = x.PaymentMethod.Name,
                    Description = x.Description,
                    Amount = x.Amount,
                    IsIncome = x.MovementTypeId == 2,
                    IsRecurringPayment = x.RecurringPaymentId != null,
                    InstallmentCount = instCount
                };
            })
            .ToList();

        return new MovementGeneralReport
        {
            data = resultList,
            PageSize = filter.PageSize,
            PageNumber = filter.PageNumber,
        };
    }
}
