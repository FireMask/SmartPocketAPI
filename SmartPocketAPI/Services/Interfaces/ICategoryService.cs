using SmartPocketAPI.Models;
using SmartPocketAPI.ViewModels;

namespace SmartPocketAPI.Services.Interface;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync(Guid userid);
    Task<Category> CreateCategoryAsync(CategoryViewModel categoryViewModel);
    Task<bool> DeleteCategoryAsync(Guid userid, int id);
    Task<Category?> GetCategoryByIdAsync(Guid userid, int id);
}
