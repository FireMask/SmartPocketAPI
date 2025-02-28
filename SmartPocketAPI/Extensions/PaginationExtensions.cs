namespace SmartPocketAPI.Extensions;

using Microsoft.EntityFrameworkCore;
using SmartPocketAPI.ApiResponse;

public static class PaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
    }

    public static PagedResult<T> ToPagedListAsync<T>(this IEnumerable<T> result, int pageNumber, int pageSize)
    {
        var totalCount = result.Count();
        var items = result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
    }
}
