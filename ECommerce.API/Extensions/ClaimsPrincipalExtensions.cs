using System.Security.Claims;

namespace ECommerce.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retrieves the email address from the given claims principal.
        /// </summary>
        /// <param name="user">The claims principal containing the user's claims.</param>
        /// <returns>The email address associated with the claims principal, or null if not found.</returns>
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
