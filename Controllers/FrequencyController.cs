using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Database;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FrequencyController : Controller
{
    private readonly IFrequencyService _frequencyContext;
    private readonly ILogger<FrequencyController> _logger;

    public FrequencyController(IFrequencyService frequencyService, ILogger<FrequencyController> logger)
    {
        _frequencyContext = frequencyService;
        _logger = logger;
    }

    [HttpGet("/Frequencies")]
    public async Task<IResult> GetFrequencies()
    {
        try
        {
            var result = await _frequencyContext.GetFrequenciesAsync();
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiResponse(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetFrequency(int id)
    {
        try
        {
            var result = await _frequencyContext.GetFrequencyByIdAsync(id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateFrequency(FrequencyViewModel frequencyViewModel)
    {
        try
        {
            if (frequencyViewModel == null)
                throw new ArgumentNullException(nameof(frequencyViewModel));

            var result = await _frequencyContext.CreateFrequencyAsync(frequencyViewModel);
            return result.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete]
    public async Task<IResult> DeleteFrequency(int id)
    {
        try
        {
            var result = await _frequencyContext.DeleteFrequencyAsync(id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
