using ECommerce.Core.Identity.Permission;

namespace ECommerce.Infrastrucure.Repositories.Permissions;

public interface IPermissionsRepository
{
    Task<PermissionDto> GetPermissionByIdAsync(int id);
    Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
    void AddPermission(PermissionDto permission);
    Task<bool> UpdatePermission(int id, PermissionDto permissionDto);
    Task<bool> DeletePermission(int id);
}


public class PermissionsRepository : IPermissionsRepository
{
    private readonly ApplicationDBContext _context;

    public PermissionsRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<PermissionDto> GetPermissionByIdAsync(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        return permission != null ? new PermissionDto { Id = permission.Id, Name = permission.Name, Description = permission.Description } : null;
    }

    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        return _context.Permissions.Select(p => new PermissionDto { Id = p.Id, Name = p.Name, Description = p.Description }).ToList();
    }

    public void AddPermission(PermissionDto permissionDto)
    {
        var permission = new PermissionDto { Name = permissionDto.Name, Description = permissionDto.Description };
        _context.Permissions.Add(permission);
    }

    public async Task<bool> UpdatePermission(int id, PermissionDto permissionDto)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null) return false;

        permission.Name = permissionDto.Name;
        permission.Description = permissionDto.Description;
        _context.Permissions.Update(permission);
        return true;
    }

    public async Task<bool> DeletePermission(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null) return false;

        _context.Permissions.Remove(permission);
        return true;
    }
}
