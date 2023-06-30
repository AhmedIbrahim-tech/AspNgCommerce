namespace ECommerce.Infrastrucure.Data.Configurations;

public class ProductTypesConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        #region HasData
        //builder.HasData
        //    (
        //        new ProductType
        //        {
        //            Id = 1,
        //            Name = "Boards"
        //        },
        //        new ProductType
        //        {
        //            Id = 2,
        //            Name = "Hats"

        //        },
        //        new ProductType
        //        {
        //            Id = 3,
        //            Name = "Roots"

        //        },
        //        new ProductType
        //        {
        //            Id = 4,
        //            Name = "Gloves"

        //        }
        //    ); 
        #endregion
    }
}
