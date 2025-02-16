using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameSpanish { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DescriptionSpanish { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
    public Guid UserId { get; set; }
    public User User { get; set; }

    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }
    [JsonIgnore]
    public ICollection<RecurringPayment> RecurringPayments { get; set; }

}
