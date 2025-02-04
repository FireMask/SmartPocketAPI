using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;
using System.Xml.Linq;

namespace SmartPocketAPI.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly ApplicationDbContext _context;

    public PaymentMethodService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethodViewModel paymentMethodVM)
    {
        if (_context.Categories.Any(pm => pm.UserId == paymentMethodVM.UserId && pm.Name == paymentMethodVM.Name))
            throw new Exception("Payment method name already exists");

        PaymentMethod newPaymentMethod = new PaymentMethod
        {
            UserId = paymentMethodVM.UserId,
            PaymentMethodTypeId = paymentMethodVM.PaymentMethodTypeId,
            Name = paymentMethodVM.Name,
            Bank = paymentMethodVM.Bank,
            IsCreditCard = paymentMethodVM.IsCreditCard,
            DueDate = paymentMethodVM.DueDate,
            TransactionDate = paymentMethodVM.TransactionDate,
            DefaultInterestRate = paymentMethodVM.DefaultInterestRate
        };

        _context.PaymentMethods.Add(newPaymentMethod);

        await _context.SaveChangesAsync();

        return newPaymentMethod;
    }

    public async Task<bool> DeletePaymentMethodAsync(Guid userid, int id)
    {
        PaymentMethod? paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);

        if (paymentMethod == null)
            throw new Exception("Payment method does not exists");

        _context.PaymentMethods.Remove(paymentMethod);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(Guid userid, int id)
    {
        PaymentMethod? paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);
        return paymentMethod;
    }

    public async Task<List<PaymentMethod>> GetPaymentMethodsAsync(Guid userid)
    {
        var result = await _context.PaymentMethods.Where(x => x.UserId == userid).ToListAsync();
        return result;
    }
}
