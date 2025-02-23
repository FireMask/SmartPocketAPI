using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.Filters;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ReportController : Controller
{
    private readonly IReportService _reportService;
    private readonly ILogger<ReportController> _logger;

    public ReportController(IReportService reportService, ILogger<ReportController> logger)
    {
        _reportService = reportService;
        _logger = logger;
    }

    [HttpGet("/GeneralMonthlyReport")]
    public async Task<IResult> GeneralMonthlyReport(MovementFilter movementFilter)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            movementFilter.UserId = userId;
            var result = await _reportService.GetMonthlyReport(movementFilter);
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }
}
