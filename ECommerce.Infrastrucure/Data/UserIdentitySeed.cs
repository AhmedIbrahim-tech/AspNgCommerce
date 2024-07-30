using ECommerce.Core.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastrucure.Data;

public class UserIdentitySeed
{

    public static async Task SeedDefaultData(IServiceProvider service)
    {
        var userMgr = service.GetService<UserManager<IdentityUser>>();
        var roleMgr = service.GetService<RoleManager<IdentityRole>>();
        //adding some roles to db
        await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

        // create admin user

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

        var userInDb = await userMgr.FindByEmailAsync(user.Email);
        if (userInDb is null)
        {
            await userMgr.CreateAsync(user, "Admin@123");
            await userMgr.AddToRoleAsync(user, Roles.Admin.ToString());
        }
    }

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
