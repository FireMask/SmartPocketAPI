
using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.ViewModels;
using System.Security.Claims;

namespace SmartPocketAPI.Services;

public class CategoryService : ICategoryService
{
    public ApplicationDbContext _context { get; }

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesAsync(Guid userid)
    {
        var result = await _context.Categories.Where(x => x.UserId == userid || x.IsDefault).ToListAsync();
        return result;
    }


    public async Task<Category> CreateCategoryAsync(CategoryViewModel categoryvm)
    {
        if (_context.Categories.Any(cat => cat.Name == categoryvm.Name && cat.UserId == categoryvm.UserId))
            throw new Exception("Category name already exists");

        Category newCategory = new Category
        {
            Name = categoryvm.Name,
            NameSpanish = categoryvm.NameSpanish,
            Description = categoryvm.Description,
            DescriptionSpanish = categoryvm.DescriptionSpanish,
            UserId = categoryvm.UserId
        };

        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == categoryvm.UserId);
        if (user is not null && user.IsAdmin)
            newCategory.IsDefault = true;

        _context.Categories.Add(newCategory);

        await _context.SaveChangesAsync();

        return newCategory;
    }

    public async Task<bool> DeleteCategoryAsync(Guid userid, int id)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);

        if (category == null)
            throw new Exception("Category does not exists");

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid userid, int id)
    {
        Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userid);
        return category;
    }
    
    public async Task<object> GetTopCategories(Guid userid)
    {
        int maxCategories = 3;

        var categories = await (
            from m in _context.Movements
            join c in _context.Categories on m.CategoryId equals c.Id
            where m.UserId == userid && (c.UserId == userid || c.IsDefault)
            group m by new { m.CategoryId, c.Name } into g
            select new
            {
                Id = g.Key.CategoryId,
                Name = g.Key.Name,
            })
            .Take(maxCategories)
            .ToListAsync();

        return categories;
    }

}