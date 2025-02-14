
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services;

public class FrequencyService : IFrequencyService
{
    public ApplicationDbContext _context { get; }

    public FrequencyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Frequency>> GetFrequenciesAsync()
    {
        return await _context.Frequency.ToListAsync();
    }

    public async Task<Frequency?> CreateFrequencyAsync(FrequencyViewModel freqvm)
    {
        if (_context.Frequency.Any(freq => freq.Name == freqvm.Name))
            return null;

        Frequency frequency = new Frequency
        {
            Name = freqvm.Name,
            NameSpanish = freqvm.NameSpanish
        };

        _context.Frequency.Add(frequency);

        await _context.SaveChangesAsync();

        return frequency;
    }

    public async Task<bool> DeleteFrequencyAsync(int id)
    {
        try
        {
            Frequency? freq = await _context.Frequency.FirstOrDefaultAsync(x => x.Id == id);

            if (freq == null)
                return false;

            _context.Frequency.Remove(freq);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }

    public async Task<Frequency?> GetFrequencyByIdAsync(int id)
    {
        Frequency? freq = await _context.Frequency.FirstOrDefaultAsync(x => x.Id == id);

        if (freq == null)
            return null;

        return freq;
    }

}