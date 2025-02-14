namespace SmartPocketAPI.ViewModels.Filters;

public class OrderByModel<T> : PaginationModel where T : struct, Enum
{
    // Sorting and Pagination
    public T SortBy { get; set; }
    public bool IsDescending { get; set; } = true;

    public OrderByModel()
    {
        SortBy = default;
    }
}

public class PaginationModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}