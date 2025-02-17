
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;
using System.Security.Claims;

namespace SmartPocketAPI.Services;

public class RecurringPaymentService : IRecurringPaymentService
{
    public ApplicationDbContext _context { get; }

    public RecurringPaymentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecurringPayment>> GetRecurringPaymentsAsync(Guid userid)
    {
        var result = await _context.RecurringPayments.Where(x => x.UserId == userid).ToListAsync();
        return result;
    }

    public async Task<RecurringPayment> CreateRecurringPaymentAsync(RecurringPaymentViewModel recurringvm)
    {
        RecurringPayment newRecurringPayment = new RecurringPayment
        {
            Description = recurringvm.Description,
            IsInterestFreePayment = recurringvm.IsInterestFreePayment,
            InstallmentCount = recurringvm.InstallmentCount ?? -1,
            NextInstallmentCount = recurringvm.NextInstallmentCount,
            InstallmentAmount = recurringvm.InstallmentAmount,
            InstallmentAmountPerPeriod = recurringvm.InstallmentAmount / (recurringvm.InstallmentCount ?? 1),
            StartDate = recurringvm.StartDate,
            EndDate = recurringvm.EndDate,
            NextInstallmentDate = recurringvm.StartDate,
            LastInstallmentDate = null,
            IsActive = recurringvm.IsActive,
            CategoryId = recurringvm.CategoryId,
            PaymentMethodId = recurringvm.PaymentMethodId,
            MovementTypeId = recurringvm.MovementTypeId,
            CreditCardPaymentId = recurringvm.CreditCardPaymentId,
            UserId = recurringvm.UserId,
            FrecuencyId = recurringvm.FrecuencyId,
        };

        _context.RecurringPayments.Add(newRecurringPayment);

        await _context.SaveChangesAsync();

        return newRecurringPayment;
    }

    public async Task<bool> DeleteRecurringPaymentAsync(Guid userid, int id)
    {
        RecurringPayment? recurringPayment = await _context.RecurringPayments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);

        if (recurringPayment == null)
            throw new Exception("Recurring payment does not exists");

        _context.RecurringPayments.Remove(recurringPayment);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<RecurringPayment?> GetRecurringPaymentByIdAsync(Guid userid, int id)
    {
        RecurringPayment? recurringPayment = await _context.RecurringPayments.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);
        return recurringPayment;
    }

    public async Task<RecurringPayment> UpdateRecurringPaymentAsync(RecurringPaymentViewModel recurringPaymentViewModel)
    {
        RecurringPayment? recurringPayment = await GetRecurringPaymentByIdAsync(recurringPaymentViewModel.UserId, recurringPaymentViewModel.Id);

        if (recurringPayment == null)
            throw new ArgumentNullException("Recurrent payment does not exists");

        recurringPayment.IsInterestFreePayment = recurringPaymentViewModel.IsInterestFreePayment;
        recurringPayment.InstallmentCount = recurringPaymentViewModel.InstallmentCount ?? -1;
        recurringPayment.InstallmentAmount = recurringPaymentViewModel.InstallmentAmount;
        recurringPayment.StartDate = recurringPaymentViewModel.StartDate;
        recurringPayment.EndDate = recurringPaymentViewModel.EndDate;
        recurringPayment.Frequency = recurringPayment.Frequency;

        _context.RecurringPayments.Update(recurringPayment);

        await _context.SaveChangesAsync();

        return recurringPayment;
    }
}