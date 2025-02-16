using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class MovementType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameSpanish { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }

    [JsonIgnore]
    public ICollection<RecurringPayment> RecurringPayments { get; set; }
}
