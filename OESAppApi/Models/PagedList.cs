namespace OESAppApi.Models;

public class PagedList<T>
{
    public PagedList(int pageSize, int page, int totalItemCount, List<T> items)
    {
        PageSize = pageSize;
        Page = page;
        TotalItemCount = totalItemCount;
        Items = items;
    }

    public int PageSize { get; }
    public int Page { get; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page * PageSize < TotalItemCount;
    public int TotalItemCount { get; }
    public List<T> Items { get; }
}
