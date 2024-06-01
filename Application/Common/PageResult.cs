namespace PolisProReminder.Application.Common;

public class PageResult<T>
{
    public PageResult(IEnumerable<T> items, int totalCount, int pageSize, int pageIndex)
    {
        Items = items;
        TotalItemsCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = pageSize * pageIndex + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
