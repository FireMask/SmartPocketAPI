using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class RecurringPayment
{
    public int Id { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public bool IsInterestFreePayment { get; set; }
    public int InstallmentCount { get; set; }
    public int CurrentInstallmentCount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public decimal InstallmentAmountPerPeriod { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateOnly NextInstallmentDate { get; set; }  
    public DateOnly? LastInstallmentDate { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public int PaymentMethodId { get; set; }
    public int MovementTypeId { get; set; }
    public int? CreditCardPaymentId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int FrecuencyId { get; set; }
    public Frequency Frequency { get; set; }

    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }

}
