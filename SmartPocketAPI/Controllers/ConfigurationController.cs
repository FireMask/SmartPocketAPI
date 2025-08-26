using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ConfigurationController : Controller
{
    private readonly IConfigurationService _userConfigurationService;
    private readonly ILogger<ConfigurationController> _logger;

    public ConfigurationController(IConfigurationService userConfigurationService, ILogger<ConfigurationController> logger)
    {
        _userConfigurationService = userConfigurationService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IResult> GetAllConfigurations()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var configs = await _userConfigurationService.GetAllConfigurationsAsync(userId);
            return configs.ToApiResponse(Constants.FETCH_SUCCESS);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{key}")]
    public async Task<IResult> GetConfiguration(string key)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var config = await _userConfigurationService.GetConfigurationAsync(userId, key);
            return config.ToApiResponse(Constants.FETCH_SUCCESS);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> AddOrUpdateConfiguration([FromBody] ConfigurationViewModel config)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _userConfigurationService.AddOrUpdateConfigurationAsync(userId, config.Key, config.Value);
            return result.ToApiResponse(Constants.UPDATE_SUCCESS);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.UPDATE_ERROR);
        }
    }
}
