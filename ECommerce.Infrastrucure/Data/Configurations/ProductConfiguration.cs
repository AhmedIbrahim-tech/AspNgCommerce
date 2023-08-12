using ECommerce.Core.Entities;

namespace ECommerce.Infrastrucure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Price).HasColumnType("decimal");
        builder.Property(x => x.PictureURL).IsRequired();
        builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x => x.ProductBrandID);
        builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.ProductTypeID);

        #region HasData
        //builder.HasData
        //    (
        //        new Product
        //        {
        //            Id = 1,
        //            Name = "Angular Speedster Board 2000",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 200,
        //            PictureURL = "images/Products/sb-ang1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 1
        //        },
        //        new Product
        //        {   
        //            Id = 2,
        //            Name = "Green Angular Board 3000",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 150,
        //            PictureURL = "images/Products/sb-ang2.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 1
        //        },
        //        new Product
        //        {
        //            Id = 3,
        //            Name = "Core Board Speed Rush 3",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 180,
        //            PictureURL = "images/Products/sb-core1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 2

        //        },
        //        new Product
        //        {
        //            Id = 4,
        //            Name = "Net Core Super Board",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 300,
        //            PictureURL = "images/Products/sb-ang2.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 2

        //        },
        //        new Product
        //        {
        //            Id = 5,
        //            Name = "React Board Super Whizzy Fast",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 250,
        //            PictureURL = "images/Products/sb-react1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 4

        //        },
        //        new Product
        //        {
        //            Id = 6,
        //            Name = "TypeScript Entity Board",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 120,
        //            PictureURL = "images/Products/sb-ang1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 2

        //        },
        //        new Product
        //        {
        //            Id = 7,
        //            Name = "Core Blue Hat",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 10,
        //            PictureURL = "images/Products/hat-core1.png",
        //            ProductTypeID = 2,
        //            ProductBrandID = 2

        //        },
        //        new Product
        //        {
        //            Id = 8,
        //            Name = "Speedster Board 2000",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 200,
        //            PictureURL = "images/Products/sb-ang1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 1

        //        },
        //        new Product
        //        {
        //            Id = 9,
        //            Name = "Green Board 3000",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 150,
        //            PictureURL = "images/Products/sb-ang2.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 1

        //        },
        //        new Product
        //        {
        //            Id = 10,
        //            Name = "Board Speed Rush 3",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 180,
        //            PictureURL = "images/Products/sb-core1.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 2

        //        },
        //        new Product
        //        {
        //            Id = 11,
        //            Name = "Super Board",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 300,
        //            PictureURL = "images/Products/sb-ang2.png",
        //            ProductTypeID = 1,
        //            ProductBrandID = 3

        //        },
        //        new Product
        //        {
        //            Id = 12,
        //            Name = "Board Super Whizzy Fast",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 250,
        //            PictureURL = "images/Products/sb-react1.png",
        //            ProductTypeID = 4,
        //            ProductBrandID = 4

        //        },
        //        new Product
        //        {
        //            Id = 13,
        //            Name = "Entity Board",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 120,
        //            PictureURL = "images/Products/sb-ang1.png",
        //            ProductTypeID = 4,
        //            ProductBrandID = 4

        //        },
        //        new Product
        //        {
        //            Id = 14,
        //            Name = "Blue Hat",
        //            Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Maxime mollitia,molestiae quas vel sint commodi repudiandae consequuntur",
        //            Price = 10,
        //            PictureURL = "images/Products/hat-core1.png",
        //            ProductTypeID = 3,
        //            ProductBrandID = 6

        //        }
        //    );

        #endregion    
    }
}
