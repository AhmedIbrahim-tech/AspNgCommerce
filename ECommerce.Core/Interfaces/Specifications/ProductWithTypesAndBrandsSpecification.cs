namespace ECommerce.Core.Interfaces.Specifications;

public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    public ProductWithTypesAndBrandsSpecification()
    {
        AddIncludes(x => x.ProductType);
        AddIncludes(x => x.ProductBrand);
    }

    public ProductWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
    {
        AddIncludes(x => x.ProductType);
        AddIncludes(x => x.ProductBrand);
    }
}
