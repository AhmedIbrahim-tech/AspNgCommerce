namespace ECommerce.Core.CustomEntities;

public class Pagination<T> where T : class
{
    public Pagination(int CurrentPage, int pageSize, int totalCount, IReadOnlyList<T> Data)
    {
        this.PageIndex = CurrentPage;
        this.PageSize = pageSize;
        this.Count = totalCount;
        this.Data = Data;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<T> Data { get; set; }
}



