namespace ECommerce.Core.Identity.Permission;

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<RoleDto> Roles { get; set; }  // Optional, depends on your domain design
}
