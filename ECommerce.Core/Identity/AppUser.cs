using ECommerce.Core.Identity.Authorization;

namespace ECommerce.Core.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }


}
