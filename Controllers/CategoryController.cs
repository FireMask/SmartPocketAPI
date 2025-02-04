using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet("/Categories")]
    public async Task<IResult> GetCategories()
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _categoryService.GetCategoriesAsync(userId);
            return Results.Json(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetCateogry(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            Category? cat = await _categoryService.GetCategoryByIdAsync(userId, id);
            return Results.Ok(cat);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.NoContent();
    }

    [HttpPost]
    public async Task<IResult> CreateCategory(CategoryViewModel categoryViewModel)
    {
        try
        {
            if (categoryViewModel == null)
                throw new ArgumentNullException(nameof(categoryViewModel));

            categoryViewModel.UserId = HttpContext.GetUserId();

            Category newCategory = await _categoryService.CreateCategoryAsync(categoryViewModel);

            return Results.Ok(newCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }

    [HttpDelete]
    public async Task<IResult> DeleteCategory(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _categoryService.DeleteCategoryAsync(userid, id);

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }

        return Results.BadRequest();
    }
}
