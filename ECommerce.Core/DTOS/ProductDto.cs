namespace ECommerce.Core.DTOS;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureURL { get; set; }
    public string ProductType { get; set; }
    public string ProductBrand { get; set; }
}
