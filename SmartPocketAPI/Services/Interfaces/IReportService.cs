using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;
using SmartPocketAPI.ViewModels.Filters;

namespace SmartPocketAPI.Services.Interfaces;

public interface IReportService
{
    Task<MovementGeneralReport> GetMonthlyReport(MovementFilter filter);
}
