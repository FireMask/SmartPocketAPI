using SmartPocketAPI.ViewModels;
using System;
using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string VerifyCode {  get; set; } = string.Empty;
    public bool IsPremium { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

    [JsonIgnore]
    public ICollection<Category> Categories { get; set; }
    [JsonIgnore]
    public ICollection<PaymentMethod> PaymentMethods { get; set; }
    [JsonIgnore]
    public ICollection<RecurringPayment> RecurringPayments { get; set; }
    [JsonIgnore]
    public ICollection<Movement> Movements { get; set; }

    public SimpleUserDto DtoModel()
    {
        return new SimpleUserDto() {
            Email = this.Name,
            Name = this.Email
        };
    }
}
