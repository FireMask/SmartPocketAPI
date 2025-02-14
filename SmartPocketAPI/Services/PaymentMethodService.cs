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

    public async Task<List<PaymentMethod>> GetPaymentMethodsAsync(Guid userid)
    {
        var result = await _context.PaymentMethods
            .Where(x => x.UserId == userid || x.IsDefault)
            .Include(x => x.PaymentMethodType)
            .ToListAsync();
        return result;
    }

    public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(Guid userid, int id)
    {
        PaymentMethod? paymentMethod = await _context.PaymentMethods
            .Where(x => x.Id == id && (x.UserId == userid || x.IsDefault))
            .Include(x => x.PaymentMethodType)
            .FirstOrDefaultAsync();
        return paymentMethod;
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

        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == paymentMethodVM.UserId);
        if(user is not null && user.IsAdmin)
            newPaymentMethod.IsDefault = true;

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
}
