namespace ECommerce.Infrastrucure.Data.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{

    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        #region HasData
        //builder.HasData(
        //     new AppUser
        //     {
        //         Id = "1",
        //         DisplayName = "Ahmed Eprahim",
        //         Email = "ebrahema89859@gmail.com",
        //         UserName = "ebrahema89859",
        //         PasswordHash = "$2y$10$PxkzUESYInsDdEDCbYuIL.Hv6PdC6i0r9xfuhJP0.2YQTi26A/B9C"
        //     }
        //    );
        #endregion
    }
}
