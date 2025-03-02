using SmartPocketAPI.Models;

namespace SmartPocketAPI.Services.Interfaces;

public interface IConfigurationService
{
    public Task<Configuration> GetConfigurationAsync(Guid userId, string key);
    public Task<IEnumerable<UserConfig>> GetAllConfigurationsAsync(Guid userId);
    public Task<Configuration> AddOrUpdateConfigurationAsync(Guid userId, string key, string value);
    public Task<bool> DeleteConfigurationAsync(Guid userId, string key);
}
