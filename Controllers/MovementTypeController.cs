using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MovementTypeController : Controller
{
    private readonly IMovementTypeService _movementTypeService;

    public MovementTypeController(IMovementTypeService movementTypeService)
    {
        _movementTypeService = movementTypeService;
    }

    [HttpGet("/MovementTypes")]
    public async Task<IResult> GetMovementTypes()
    {
        return Results.Json(await _movementTypeService.GetMovementTypesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetMovementType(int id)
    {
        MovementType? move = await _movementTypeService.GetMovementTypeByIdAsync(id);

        if (move == null)
            return Results.NoContent();
        else
            return Results.Json(move);
    }

    [HttpPost]
    public async Task<IResult> CreateFrequency(MovementTypeViewModel movementTypeVM)
    {
        if (movementTypeVM == null)
            return Results.BadRequest();

        MovementType? newMove = await _movementTypeService.CreateMovementTypeAsync(movementTypeVM);

        if (newMove == null)
            return Results.BadRequest();
        else
            return Results.Ok(newMove);
    }

    [HttpDelete]
    public async Task<IResult> DeleteFrequency(int id)
    {

        if (await _movementTypeService.DeleteMovementTypeAsync(id))
            return Results.Ok();
        else
            return Results.BadRequest();
    }
}
