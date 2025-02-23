using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MovementTypeController : Controller
{
    private readonly IMovementTypeService _movementTypeService;
    private readonly ILogger<MovementTypeController> _logger;

    public MovementTypeController(IMovementTypeService movementTypeService, ILogger<MovementTypeController> logger)
    {
        _movementTypeService = movementTypeService;
        _logger = logger;
    }

    [HttpGet("/MovementTypes")]
    public async Task<IResult> GetMovementTypes()
    {
        try
        {
            var result = await _movementTypeService.GetMovementTypesAsync();
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetMovementType(int id)
    {
        try
        {
            var result = await _movementTypeService.GetMovementTypeByIdAsync(id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateMovement(MovementTypeViewModel movementTypeVM)
    {
        try
        {
            if (movementTypeVM == null)
                throw new ArgumentNullException(nameof(movementTypeVM));

            var result = await _movementTypeService.CreateMovementTypeAsync(movementTypeVM);
            return result.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete]
    public async Task<IResult> DeleteMovement(int id)
    {
        try
        {
            var result = await _movementTypeService.DeleteMovementTypeAsync(id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
