namespace ECommerce.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureURL { get; set; }

    public int ProductTypeID { get; set; }
    public ProductType ProductType { get; set; }

    public int ProductBrandID { get; set; }
    public ProductBrand ProductBrand { get; set; }
}
