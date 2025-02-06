namespace SmartPocketAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<Category> Categories { get; set; }
    public ICollection<PaymentMethod> PaymentMethods { get; set; }
    public ICollection<RecurringPayment> RecurringPayments { get; set; }
    public ICollection<Movement> Movements { get; set; }
}
