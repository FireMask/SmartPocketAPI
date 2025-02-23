namespace SmartPocketAPI.ViewModels.RequestModels;

public class MovementsRequest : RequestQuery
{
    public string? Search { get; set; }
    public int[]? CategoryId { get; set; }
    public int[]? PaymentMethodId { get; set; }
    public int[]? MovementTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
