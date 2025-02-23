namespace SmartPocketAPI.ViewModels.RequestModels;

public class RecurringPaymentsRequest : RequestQuery
{
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
