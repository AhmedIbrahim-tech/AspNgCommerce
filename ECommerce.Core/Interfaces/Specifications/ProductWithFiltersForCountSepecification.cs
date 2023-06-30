namespace ECommerce.Core.Interfaces.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams param)
        : base(x =>
        (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
        (!param.BrandId.HasValue || x.ProductBrandID == param.BrandId) &&
        (!param.TypeId.HasValue || x.ProductTypeID == param.TypeId)
        )
    {

    }
}
