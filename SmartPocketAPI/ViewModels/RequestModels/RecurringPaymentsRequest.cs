namespace SmartPocketAPI.ViewModels.RequestModels;

public class RecurringPaymentsRequest : RequestQuery
{
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int[]? PaymentMethodId { get; set; }
    public DateOnly? UntilDate { get; set; }
    public bool HasPendingMovements { get; set; } = false;
}
