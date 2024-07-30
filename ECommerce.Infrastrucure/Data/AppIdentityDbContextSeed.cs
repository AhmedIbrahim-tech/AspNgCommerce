using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastrucure.Data;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser()
            {
                DisplayName = "Ahmed Eprahim",
                Email = "ebrahema89859@gmail.com",
                UserName = "ebrahema89859@gmail.com",
                Address = new Address()
                {
                    FirstName = "Ahmed",
                    LastName = "Eprahim",
                    Street = "Musa bin Nasser",
                    City = "Cairo",
                    State = "EG",
                    Zipcode = "71111"
                }
            };

            await userManager.CreateAsync(user, "p@ssw0rd");
        }

    }
}
