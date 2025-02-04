using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FrequencyController : Controller
{
    private readonly IFrequencyService _frequencyContext;

    public FrequencyController(IFrequencyService frequencyService)
    {
        _frequencyContext = frequencyService;
    }

    [HttpGet("/Frequencies")]
    public async Task<IResult> GetFrequencies()
    {
        return Results.Json(await _frequencyContext.GetFrequenciesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetFrequency(int id)
    {
        Frequency? freq = await _frequencyContext.GetFrequencyByIdAsync(id);

        if (freq == null)
            return Results.NoContent();
        else
            return Results.Json(freq);
    }

    [HttpPost]
    public async Task<IResult> CreateFrequency(FrequencyViewModel frequencyViewModel)
    {
        if (frequencyViewModel == null)
            return Results.BadRequest();

        Frequency? newFreq = await _frequencyContext.CreateFrequencyAsync(frequencyViewModel);

        if (newFreq == null)
            return Results.BadRequest();
        else
            return Results.Ok(newFreq);
    }

    [HttpDelete]
    public async Task<IResult> DeleteFrequency(int id)
    {

        if (await _frequencyContext.DeleteFrequencyAsync(id))
            return Results.Ok();
        else
            return Results.BadRequest();
    }
}
