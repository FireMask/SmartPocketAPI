using SmartPocketAPI.Models;

namespace SmartPocketAPI.ViewModels;

public class PaymentMethodViewModel
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Bank { get; set; } = string.Empty;
    public bool IsCreditCard { get; set; } = false;
    public int DueDate { get; set; }
    public int TransactionDate { get; set; }
    public decimal DefaultInterestRate { get; set; }
}