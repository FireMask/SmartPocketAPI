using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class PaymentMethodType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameSpanish { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<PaymentMethod> PaymentMethods { get; set; }
}
