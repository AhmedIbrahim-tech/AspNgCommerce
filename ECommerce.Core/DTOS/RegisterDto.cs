namespace ECommerce.Core.DTOS;

public class RegisterDto : LoginDto
{
    [Required]
    public string DisplayName { get; set; }
}



