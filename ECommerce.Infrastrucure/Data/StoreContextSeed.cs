using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Infrastrucure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(ApplicationDBContext context)
    {

        if (!context.ProductBrands.Any())
        {
            var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            foreach (var item in brands)
            {
                context.ProductBrands.Add(item);
            }

            await context.SaveChangesAsync();
        }
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            foreach (var item in types)
            {
                context.ProductTypes.Add(item);
            }

            await context.SaveChangesAsync();
        }
        if (!context.Products.Any())
        {
            var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            foreach (var item in products)
            {
                context.Products.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
