namespace SmartPocketAPI.Options;

public class ConfigurationOptions
{
    public string ApiName { get; set; }
    public int MaxRequestsPerMinute { get; set; }
    public string ConnectionString { get; set; }
    public int CommandTimeOut { get; set; }
}
