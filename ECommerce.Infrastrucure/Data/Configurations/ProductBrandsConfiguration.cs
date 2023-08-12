using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Data.Configurations;

public class ProductBrandsConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        #region HasData
        //builder.HasData(

        //        new ProductBrand { Id = 1, Name = "Angular" },
        //        new ProductBrand { Id = 2, Name = "NetCore" },
        //        new ProductBrand { Id = 3, Name = "VS Code" },
        //        new ProductBrand { Id = 4, Name = "React" },
        //        new ProductBrand { Id = 5, Name = "TypeScript" },
        //        new ProductBrand { Id = 6, Name = "Redis" }
        //    );
        #endregion
    }
}
