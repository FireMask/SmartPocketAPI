namespace SmartPocketAPI.ViewModels;

public class CardMonthSummaryViewModel
{
    public string CardName { get; set; } = string.Empty;
    public decimal TotalSum { get; set; }
    public DateOnly TransactionDate { get; set; }
    public DateOnly DueDate { get; set; }
}