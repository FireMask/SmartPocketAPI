namespace SmartPocketAPI.Options;

public class ConfigurationOptions
{
    public string ApiName { get; set; } = string.Empty;
    public int MaxRequestsPerMinute { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public int CommandTimeOut { get; set; }
}
