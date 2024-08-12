namespace ECommerce.Core.QueryFilters;

public class ProductsQueryFilter
{
    public string Name { get; set; }
    public int? BrandId { get; set; }
    public int? Categoryid { get; set; }
    public int? TypeId { get; set; }
    public string Description { get; set; }
    private string _search;
    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }

    private const int MaxPageSize = 50;
    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int PageNumber { get; set; } = 1;
    public string Sort { get; set; } = "ASC";
}
