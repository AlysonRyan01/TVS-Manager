namespace TVS_App.Domain.Shared;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? TotalPages { get; set; }

    public PaginatedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize, int? totalPages = null)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
    }
}