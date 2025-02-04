
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services;

public class PaymentMethodTypeService : IPaymentMethodTypeService
{
    public ApplicationDbContext _context { get; }

    public PaymentMethodTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentMethodType>> GetPaymentMethodTypesAsync()
    {
        return await _context.PaymentMethodType.ToListAsync();
    }

    public async Task<PaymentMethodType?> CreatePaymentMethodTypeAsync(PaymentMethodTypeViewModel paymentMethodTypeViewModel)
    {
        if (_context.PaymentMethodType.Any(move => move.Name == paymentMethodTypeViewModel.Name))
            return null;

        PaymentMethodType paymentMethod = new PaymentMethodType
        {
            Name = paymentMethodTypeViewModel.Name,
            NameSpanish = paymentMethodTypeViewModel.NameSpanish
        };

        _context.PaymentMethodType.Add(paymentMethod);

        await _context.SaveChangesAsync();

        return paymentMethod;
    }

    public async Task<bool> DeletePaymentMethodTypeAsync(int id)
    {
        try
        {
            PaymentMethodType? payMethod = await _context.PaymentMethodType.FirstOrDefaultAsync(x => x.Id == id);

            if (payMethod == null)
                return false;

            _context.PaymentMethodType.Remove(payMethod);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }

    public async Task<PaymentMethodType?> GetPaymentMethodTypeByIdAsync(int id)
    {
        PaymentMethodType? payMethod = await _context.PaymentMethodType.FirstOrDefaultAsync(x => x.Id == id);

        if (payMethod == null)
            return null;

        return payMethod;
    }

}