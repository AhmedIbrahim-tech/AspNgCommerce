namespace ECommerce.Core.Identity;

public class AssignRoleDto
{
    public string Email { get; set; }
    public string RoleName { get; set; }
}


public class UserResponseDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool EmailVerified { get; set; }
    public string Token { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }
}

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<PermissionDto> Permissions { get; set; }
}

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}


public class VerifyEmailDto
{
    public string Email { get; set; }
    public string Otp { get; set; }
}


public class OtpDetails
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Otp { get; set; }
    public string Token { get; set; } // Optional: Used for email confirmation links
    public DateTime ExpiresAt { get; set; }
    public bool IsValid { get; set; } // Flag to check if OTP is still valid
}


public class EmailCheckDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}



public class RetrievePasswordRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Otp { get; set; }
}


public class ChangePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
