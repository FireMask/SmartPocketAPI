namespace SmartPocketAPI.ViewModels;

public class RecurringPaymentViewModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsInterestFreePayment { get; set; }
    public required int InstallmentCount { get; set; }
    public required decimal InstallmentAmount { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required Guid UserId { get; set; }
    public required int FrecuencyId { get; set; }
}