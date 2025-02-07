using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Database;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class MovementController : Controller
{
    private readonly IMovementService _movementService;
    private readonly ILogger<MovementController> _logger;

    public MovementController(IMovementService movementService, ILogger<MovementController> logger)
    {
        _movementService = movementService;
        _logger = logger;
    }

    [HttpGet("/Movements")]
    public async Task<IResult> GetMovementTypes()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetMovementsAsync(userId);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetMovement(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.GetMovementByIdAsync(userId, id);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateMovement(MovementViewModel movementViewModel)
    {
        try
        {
            if (movementViewModel == null)
                throw new ArgumentNullException(nameof(movementViewModel));

            movementViewModel.UserId = HttpContext.GetUserId();
            var result = await _movementService.CreateMovementAsync(movementViewModel);
            return result.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            if(ex.InnerException != null)
                return ex.InnerException.Message.ToApiError(Constants.INSERT_ERROR);
            else
                return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteMovement(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _movementService.DeleteMovementAsync(userId, id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
