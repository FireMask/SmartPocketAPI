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

    public async Task<Configuration> GetConfigurationAsync(Guid userId, string key)
    {
        var userConfig = await _context.UserConfigurations
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.Key == key);

        if (userConfig != null)
        {
            return userConfig;
        }

        if (!DefaultConfigurationsUser.KeyValues.ContainsKey(key))
            return null;
        
        return new Configuration
        {
            UserId = userId,
            Key = key,
            Value = DefaultConfigurationsUser.KeyValues[key]
        };
    }

    public async Task<IEnumerable<UserConfig>> GetAllConfigurationsAsync(Guid userId)
    {
        var userConfigs = await _context.UserConfigurations
            .Where(uc => uc.UserId == userId)
            .ToListAsync();

        var userConfigDict = userConfigs.ToDictionary(uc => uc.Key, uc => uc.Value);

        // Merge with default values
        var allConfigs = DefaultConfigurationsUser.KeyValues.Select(defaultConfig =>
            new Configuration
            {
                UserId = userId,
                Key = defaultConfig.Key,
                Value = userConfigDict.ContainsKey(defaultConfig.Key)
                    ? userConfigDict[defaultConfig.Key]  // Use user's value if exists
                    : defaultConfig.Value               // Otherwise, use default
            })
            .Select(x => x.ToDto())
            .ToList();

        return allConfigs;
    }

    public async Task<Configuration> AddOrUpdateConfigurationAsync(Guid userId, string key, string value)
    {
        var config = await GetConfigurationAsync(userId, key);

        if (config == null || config.Id == 0)
        {
            config = new Configuration { UserId = userId, Key = key, Value = value };
            _context.UserConfigurations.Add(config);
        }
        else
        {
            config.Value = value;
            _context.UserConfigurations.Update(config);
        }

        await _context.SaveChangesAsync();

        return config;
    }

    public async Task<bool> DeleteConfigurationAsync(Guid userId, string key)
    {
        var config = await GetConfigurationAsync(userId, key);

        if (config == null || config.Id == 0)
            throw new Exception("Configuration does not exists");

        _context.UserConfigurations.Remove(config);
        
        await _context.SaveChangesAsync();

        return true;
    }
}
