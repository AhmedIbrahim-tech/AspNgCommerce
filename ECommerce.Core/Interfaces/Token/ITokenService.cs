namespace ECommerce.Core.Interfaces.Token;

public interface ITokenService
{
    string CreateToken(AppUser user);
}