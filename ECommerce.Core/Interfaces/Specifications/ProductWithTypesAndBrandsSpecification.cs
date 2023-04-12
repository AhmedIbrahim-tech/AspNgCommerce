namespace ECommerce.Core.Interfaces.Specifications;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification(ProductSpecParams param)
        : base (x=> 
        (string.IsNullOrEmpty(param.Search) || x.Name.ToLower().Contains(param.Search)) &&
        (!param.BrandId.HasValue || x.ProductBrandID == param.BrandId) && 
        (!param.TypeId.HasValue || x.ProductTypeID == param.TypeId)
        )
    {
        AddIncludes(x => x.ProductType);
        AddIncludes(x => x.ProductBrand);
        AddorderbyExpression(x => x.Name);
        AddorderbyDescendingExpression(x => x.Name);
        ApplyPaging(param.PageSize * (param.PageIndex - 1), param.PageSize);

        if (!string.IsNullOrEmpty(param.Sort))
        {
            switch (param.Sort)
            {
                case "priceAsc":
                    AddorderbyExpression(x=>x.Price);
                    break;
                case "priceDesc":
                    AddorderbyDescendingExpression(x => x.Price);
                    break;
                default:
                    AddorderbyExpression(x => x.Name);
                    break;
            }
        }
    }

    public ProductWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddIncludes(x => x.ProductType);
        AddIncludes(x => x.ProductBrand);
    }
}
