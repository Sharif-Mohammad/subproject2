namespace Business.Models.Common;

public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; private set; }
    public int TotalItems { get; private set; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public string NextPageUrl { get; private set; }
    public string PreviousPageUrl { get; private set; }

    public static PaginatedResult<T> Create(IEnumerable<T> items, int page, int pageSize,string apiBasePath,int totalItems = 0)
    {
        var result = new PaginatedResult<T>
        {
            Items = items,
             TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            NextPageUrl = totalItems > page * pageSize ? $"/api/{apiBasePath}?page={page + 1}&pageSize={pageSize}" : null,
           // NextPageUrl = items.Any() ?  $"/api/{apiBasePath}page={page + 1}&pageSize={pageSize}" : null,
            PreviousPageUrl = page > 1 ? $"/api/{apiBasePath}page={page - 1}&pageSize={pageSize}" : null
        };

        return result;
    }
}