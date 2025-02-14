namespace SmartPocketAPI.ViewModels.Filters;

public class MovementFilter : OrderByModel<OrderMovementBy>
{
    public Guid UserId { get; set; }
    public int? CategoryId { get; set; }
    public int? PaymentMethodId { get; set; }
    public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    public DateTime EndDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
}


public enum OrderMovementBy
{
    DATE,
    DESCRIPTION,
    AMOUNT,
    CATEGORY,
    PAYMENT_METHOD,
    RECURRING_PAYMENT,
    MOVEMENT_TYPE
}