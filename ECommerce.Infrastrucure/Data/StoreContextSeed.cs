using System.Text.Json;

namespace ECommerce.Infrastrucure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(ApplicationDBContext context)
    {
        if (!context.ProductBrands.Any())
        {
            var ProductBrands = File.ReadAllText("../ECommerce.Infrastrucure/Data/DataSeeding/Brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrands);
            context.ProductBrands.AddRange(Brands);
        }

        if (!context.Products.Any())
        {
            var ProductsList = File.ReadAllText("../ECommerce.Infrastrucure/Data/DataSeeding/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductsList);
            context.Products.AddRange(products);
        }

        if (!context.ProductTypes.Any())
        {
            var ProductTypes = File.ReadAllText("../ECommerce.Infrastrucure/Data/DataSeeding/Types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypes);
            context.ProductTypes.AddRange(types);
        }

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}
