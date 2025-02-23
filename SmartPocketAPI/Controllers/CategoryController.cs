using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPocketAPI.Extensions;
using SmartPocketAPI.Helpers;
using SmartPocketAPI.Services.Interface;
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
            return result.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetCateogry(int id)
    {
        try
        {
            Guid userId = HttpContext.GetUserId();
            var cat = await _categoryService.GetCategoryByIdAsync(userId, id);
            return cat.ToApiResponse(Constants.FETCH_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.FETCH_ERROR);
        }
    }

    [HttpPost]
    public async Task<IResult> CreateCategory(CategoryViewModel categoryViewModel)
    {
        try
        {
            if (categoryViewModel == null)
                throw new ArgumentNullException(nameof(categoryViewModel));

            categoryViewModel.UserId = HttpContext.GetUserId();
            var newCategory = await _categoryService.CreateCategoryAsync(categoryViewModel);
            return newCategory.ToApiResponse(Constants.INSERT_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.INSERT_ERROR);
        }
    }

    [HttpDelete]
    public async Task<IResult> DeleteCategory(int id)
    {
        try
        {
            Guid userid = HttpContext.GetUserId();
            var result = await _categoryService.DeleteCategoryAsync(userid, id);
            return result.ToApiResponse(Constants.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return ex.Message.ToApiError(Constants.DELETE_ERROR);
        }
    }
}
