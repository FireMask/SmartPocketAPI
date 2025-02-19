namespace SmartPocketAPI.ViewModels;

public class RecurringPaymentViewModel
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public required bool IsInterestFreePayment { get; set; }
    public required int? InstallmentCount { get; set; }
    public int NextInstallmentCount { get; set; } = 1;
    public required decimal InstallmentAmount { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public required bool IsActive { get; set; }
    public required int CategoryId { get; set; }
    public required int PaymentMethodId { get; set; }
    public required int MovementTypeId { get; set; }
    public int? CreditCardPaymentId { get; set; }
    public required int FrequencyId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly? LastInstallmentDate { get; set; }
    public DateOnly NextInstallmentDate { get; set; }
}

public class UpdateRecurringPaymentViewModel
{
    public int Id { get; set; }
    public int NextInstallmentCount { get; set; }
    public Guid UserId { get; set; }
    public DateOnly? LastInstallmentDate { get; set; }
    public DateOnly NextInstallmentDate { get; set; }
}