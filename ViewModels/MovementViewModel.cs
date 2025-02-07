using SmartPocketAPI.Models;

namespace SmartPocketAPI.ViewModels;

public class MovementViewModel
{
    public int Id { get; set; }
    public DateTime MovementDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public int CategoriId { get; set; }
    public int PaymentMethodId { get; set; }
    public int? RecurringPaymentId { get; set; }
    public int MovementTypeId { get; set; }
    public int? CreditCardPaymentId { get; set; }
}