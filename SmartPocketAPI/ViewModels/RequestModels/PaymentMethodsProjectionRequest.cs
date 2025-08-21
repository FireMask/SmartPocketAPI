namespace SmartPocketAPI.ViewModels.RequestModels;

public class PaymentMethodsProjectionRequest
{
    public int MonthsAhead { get; set; } = -2;
    public int MonthsBefore { get; set; } = 4;
}