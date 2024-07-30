namespace ECommerce.Infrastrucure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        #region HasData
        //builder.HasData(
        //     new Address
        //     {
        //         Id = 1,
        //         AppUserId = "1",
        //         FirstName = "Ahmed",
        //         LastName = "Eprahim",
        //         City = "Cairo",
        //         Street = "Musa bin Nasser",
        //         Zipcode = "71111",
        //         State = "EG",
        //     }
        //    );
        #endregion
    }
}
