using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;

namespace SmartPocketAPI.Services;

public class ConfigurationService : IConfigurationService
{
    private ApplicationDbContext _context { get; }

    public ConfigurationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ConfigurationKeyValue> GetConfigurationAsync(Guid userId, string key)
    {
        var configs = await _context.Configurations.ToListAsync();
        var userConfig = await _context.UserConfigurations
            .Include(uc => uc.Configuration)
            .FirstOrDefaultAsync(uc => uc.UserId == userId);

        if (userConfig != null)
        {
            return userConfig.toKeyValue();
        }

        Dictionary<string, string> DefaultConfigurationsUser = configs.ToDictionary(c => c.Key, c => c.DefaultValue);

        if (!DefaultConfigurationsUser.ContainsKey(key))
            throw new Exception("Configuration key does not exists");
        
        return new ConfigurationKeyValue(key, DefaultConfigurationsUser[key]);
    }

    public async Task<IEnumerable<ConfigurationKeyValue>> GetAllConfigurationsAsync(Guid userId)
    {
        var configs = await _context.Configurations.ToListAsync();
        var userConfigs = await _context.UserConfigurations
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.Configuration)
            .ToListAsync();

        Dictionary<string, string> DefaultConfigurationsUser = configs.ToDictionary(c => c.Key, c => c.DefaultValue);
        Dictionary<string, string> UserConfigurations = userConfigs.ToDictionary(uc => uc.Configuration.Key, uc => uc.Value);

        var allConfigs = DefaultConfigurationsUser
            .Select(dc => new ConfigurationKeyValue
            {
                Key = dc.Key,
                Value = UserConfigurations.ContainsKey(dc.Key) ? UserConfigurations[dc.Key] : dc.Value
            })
            .ToList();

        return allConfigs;
    }

    public async Task<ConfigurationKeyValue> AddOrUpdateConfigurationAsync(Guid userId, string key, string value)
    {
        var config = await _context.Configurations.FirstOrDefaultAsync(c => c.Key == key);
        if (config == null)
            throw new Exception("Configuration key does not exists");

        var userConfig = await _context.UserConfigurations
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ConfigurationId == config.Id);

        if (userConfig == null)
        {
            userConfig = new UserConfiguration
            {
                UserId = userId,
                ConfigurationId = config.Id,
                Value = value
            };
            _context.UserConfigurations.Add(userConfig);
        }
        else
        {
            userConfig.Value = value;
            _context.UserConfigurations.Update(userConfig);
        }

        await _context.SaveChangesAsync();
        return userConfig.toKeyValue();
    }
}
