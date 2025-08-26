using SmartPocketAPI.Models;

namespace SmartPocketAPI.Services.Interfaces;

public interface IConfigurationService
{
    public Task<ConfigurationKeyValue> GetConfigurationAsync(Guid userId, string key);
    public Task<IEnumerable<ConfigurationKeyValue>> GetAllConfigurationsAsync(Guid userId);
    public Task<ConfigurationKeyValue> AddOrUpdateConfigurationAsync(Guid userId, string key, string value);
}
