namespace SmartPocketAPI.Helpers;

public static class DefaultConfigurationsUser
{
    public static Dictionary<string, string> KeyValues = new Dictionary<string, string>
    {
        { "PaymentMethod", "1" }, //Cash
        { "Frequency", "3" }, //Monthly
    };
}
