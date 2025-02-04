using SmartPocketAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartPocketAPI.ViewModels;

public class RecurringPaymentViewModel
{
    public int Id { get; set; }
    public bool IsInterestFreePayment { get; set; }
    public int InstallmentCount { get; set; }
    public decimal InstallmentAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public int FrecuencyId { get; set; }
}