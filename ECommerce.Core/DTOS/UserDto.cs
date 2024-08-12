namespace ECommerce.Core.DTOS;

public class UserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string Otp { get; set; }
    public string PhoneNumber { get; set; }
    public string AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public IEnumerable<string> Roles { get; set; }
    // Add other fields as necessary
}
