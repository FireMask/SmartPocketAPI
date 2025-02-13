using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels.Filters;

namespace SmartPocketAPI.ViewModels;

public class MovementViewModel
{
    public int Id { get; set; }
    public DateTime MovementDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentMethodId { get; set; }
    public int? RecurringPaymentId { get; set; }
    public int MovementTypeId { get; set; }
    public int? InstallmentNumber { get; set; }
    public int? CreditCardPaymentId { get; set; }
}

public class MovementMSIViewModel
{
    public Guid? UserId { get; set; }
    public required bool IsInstallment { get; set; } = false;
    public DateTime MovementDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public int CategoryId { get; set; }
    public int PaymentMethodId { get; set; }
    public int MovementTypeId { get; set; }
    public int InstallmentCount { get; set; }
    public bool IsInterestFreePayment { get; set; }
    public required int FrecuencyId { get; set; }
    public int? CreditCardPaymentId { get; set; }
}

public class MovementReportEntity
{
    public DateTime MovementDate { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string PaymentMethodName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsIncome { get; set; }
    public bool IsRecurringPayment { get; set; }
    public string InstallmentCount { get; set; } = string.Empty;
}

public class MovementGeneralReport : PaginationModel
{
    public List<MovementReportEntity> data { get; set; } = new List<MovementReportEntity>();
}