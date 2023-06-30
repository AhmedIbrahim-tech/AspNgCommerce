using System.Text.Json;

namespace ECommerce.Infrastrucure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(ApplicationDBContext context)
    {

        if (!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../ECommerce.Infrastrucure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../ECommerce.Infrastrucure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            context.ProductTypes.AddRange(types);
        }
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../ECommerce.Infrastrucure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

    }
}
