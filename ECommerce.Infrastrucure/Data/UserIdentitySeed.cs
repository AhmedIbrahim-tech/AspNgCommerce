using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastrucure.Data;

public class UserIdentitySeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Ahmed Eprahim",
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                Address = new Address
                {
                    FirstName = "Ahmed",
                    LastName = "Eprahim",
                    Street = "17th Street",
                    City = "Bangalore",
                    State = "Katakana",
                    Zipcode = "560012"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
