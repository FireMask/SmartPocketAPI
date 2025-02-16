using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class PaymentMethod
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int PaymentMethodTypeId { get; set; }
    public PaymentMethodType PaymentMethodType { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public bool IsCreditCard { get; set; } = false;
    public int DueDate { get; set; } //Fecha limite de pago
    public int TransactionDate { get; set; } //Fecha de corte
    public decimal DefaultInterestRate { get; set; }
    public bool IsDefault {  get; set; } = false;

    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }

    [JsonIgnore]
    public ICollection<Movement> Payments { get; set; }

    [JsonIgnore]
    public ICollection<RecurringPayment> RecurringPayments { get; set; }

    [JsonIgnore]
    public ICollection<RecurringPayment> RecurringPaymentCredits { get; set; }
}
