using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class RecurringPayment
{
    public int Id { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public bool IsInterestFreePayment { get; set; }
    public int InstallmentCount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public int FrecuencyId { get; set; }
    public Frequency Frequency { get; set; }

    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }

}
