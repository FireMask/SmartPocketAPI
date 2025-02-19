namespace SmartPocketAPI.ViewModels;

public class MovementFromRecurringPaymentsViewModel
{
    public DateOnly MovementDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; } = string.Empty;
    public int RecurringPaymentId { get; set; }
    public int MovementTypeId { get; set; }
    public string MovementTypeName { get; set; } = string.Empty;
    public int InstallmentCount { get; set; }
    public int InstallmentNumber { get; set; }
    public int? CreditCardPaymentId { get; set; }
    public string CreditCardPaymentName { get; set; } = string.Empty;
    public int FrequencyId { get; set; }
    public string FrequencyName { get; set; } = string.Empty;
}