namespace SmartPocketAPI.ViewModels;

public class CardMonthSummaryViewModel
{
    public string CardName { get; set; } = string.Empty;
    public decimal TotalSum { get; set; }
    public DateOnly TransactionStartDate { get; set; }
    public DateOnly TransactionEndDate { get; set; }
    public DateOnly DueDate { get; set; }
}