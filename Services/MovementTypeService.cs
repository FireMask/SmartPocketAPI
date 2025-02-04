
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services;

public class MovementTypeService : IMovementTypeService
{
    public ApplicationDbContext _context { get; }

    public MovementTypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovementType>> GetMovementTypesAsync()
    {
        return await _context.MovementType.ToListAsync();
    }

    public async Task<MovementType?> CreateMovementTypeAsync(MovementTypeViewModel movementTypeVM)
    {
        if (_context.MovementType.Any(move => move.Name == movementTypeVM.Name))
            return null;

        MovementType movement = new MovementType
        {
            Name = movementTypeVM.Name,
            NameSpanish = movementTypeVM.NameSpanish
        };

        _context.MovementType.Add(movement);

        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<bool> DeleteMovementTypeAsync(int id)
    {
        try
        {
            MovementType? move = await _context.MovementType.FirstOrDefaultAsync(x => x.Id == id);

            if (move == null)
                return false;

            _context.MovementType.Remove(move);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }

    public async Task<MovementType?> GetMovementTypeByIdAsync(int id)
    {
        MovementType? move = await _context.MovementType.FirstOrDefaultAsync(x => x.Id == id);

        if (move == null)
            return null;

        return move;
    }

}