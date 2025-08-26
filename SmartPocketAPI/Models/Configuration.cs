using System.Text.Json.Serialization;

namespace SmartPocketAPI.Models;

public class Configuration
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string DefaultValue { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<UserConfiguration> UserConfigurations { get; set; }

}

public class UserConfiguration
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;

    public int ConfigurationId { get; set; }
    public Configuration Configuration { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ConfigurationKeyValue toKeyValue() => new(Configuration.Key, Value);
}

public record struct ConfigurationKeyValue(string Key, string Value);