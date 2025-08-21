using SmartPocketAPI.Models.General;

namespace SmartPocketAPI.Models;

public class Movement : ITimestampedEntity
{
    public int Id { get; set; }
    public DateOnly MovementDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public int? InstallmentNumber { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public int? RecurringPaymentId { get; set; }
    public RecurringPayment RecurringPayment { get; set; }

    public int MovementTypeId { get; set; }
    public MovementType MovementType { get; set; }

    public int? CreditCardPaymentId { get; set; }
    public PaymentMethod CreditCardPayment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

internal record MovementRecord(string Description, bool Pending, string Category, decimal Amount, DateOnly MovementDate);
public record MovementDocument(string Card, DateOnly Date, string Concept, decimal Amount);