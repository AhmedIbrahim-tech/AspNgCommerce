namespace ECommerce.Core.Identity;

public class RoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}
public class AssignRoleDto
{
    public string Email { get; set; }
    public string RoleName { get; set; }
}
