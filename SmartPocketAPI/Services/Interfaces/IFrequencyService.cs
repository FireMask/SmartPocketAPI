using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interfaces;

public interface IFrequencyService
{
    Task<List<Frequency>> GetFrequenciesAsync();
    Task<Frequency?> CreateFrequencyAsync(FrequencyViewModel newFrequency);
    Task<bool> DeleteFrequencyAsync(int id);
    Task<Frequency?> GetFrequencyByIdAsync(int id);
}
