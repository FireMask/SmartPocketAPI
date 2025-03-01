namespace SmartPocketAPI.ViewModels;

public class CardMonthSummaryViewModel
{
    public string CardName { get; set; } = string.Empty;
    public decimal TotalSum { get; set; }
    public decimal ThisPeriodAmount { get; set; }
    public decimal PendingMovementsAmount { get; set; }
    public DateOnly TransactionStartDate { get; set; }
    public DateOnly TransactionEndDate { get; set; }
    public DateOnly DueDate { get; set; }
}