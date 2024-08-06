namespace ECommerce.Core.DTOS;

public class UserDto
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
