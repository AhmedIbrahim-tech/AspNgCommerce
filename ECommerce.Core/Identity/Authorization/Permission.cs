namespace ECommerce.Core.Identity.Authorization;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }  // e.g., "Admin", "User"
    public string Description { get; set; }
    public List<RoleDto> Roles { get; set; }  // Optional, depends on your domain design
}
