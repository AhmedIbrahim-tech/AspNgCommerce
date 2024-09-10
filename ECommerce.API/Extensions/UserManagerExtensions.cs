using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerce.API.Extensions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Finds a user by claims principal and includes the user's address.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="user">The claims principal containing the user information.</param>
        /// <returns>The user with address, or null if not found.</returns>
        public static async Task<AppUser> FindUserByClaimsPrincipalWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var email = user.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return null;

            return await userManager.Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Finds a user by email from claims principal.
        /// </summary>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="user">The claims principal containing the user information.</param>
        /// <returns>The user, or null if not found.</returns>
        public static async Task<AppUser> FindByEmailFromClaimsPrincipalAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var email = user.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return null;

            return await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
