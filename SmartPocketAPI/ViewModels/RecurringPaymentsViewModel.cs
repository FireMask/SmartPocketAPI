namespace SmartPocketAPI.ViewModels;

public class RecurringPaymentsViewModel
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PaymentMethodName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? LastInstallmentDate { get; set; }
    public DateOnly NextInstallmentDate { get; set; }
    public string FrequencyName { get; set; } = string.Empty;
    public decimal InstallmentAmountPerPeriod { get; set; }
    public string MovementTypename { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string PaidCount { get; set; } = string.Empty;
    public bool CanDelete { get; set; }
    public List<MovementFromRecurringPaymentsViewModel> PendingMovements { get; set; } = new List<MovementFromRecurringPaymentsViewModel>();
}