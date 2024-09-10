using ECommerce.Core.Identity.Authorization;

namespace ECommerce.Infrastrucure.Repositories.Permissions;

public interface IPermissionsRepository
{
    Task<Permission> GetPermissionByIdAsync(int id);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    void AddPermission(Permission permission);
    Task<bool> UpdatePermission(int id, Permission permission);
    Task<bool> DeletePermission(int id);
}



public class PermissionsRepository : IPermissionsRepository
{
    private readonly ApplicationDBContext _context;

    public PermissionsRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await _context.Permissions.ToListAsync();
    }

    public void AddPermission(Permission permission)
    {
        _context.Permissions.Add(permission);
    }

    public async Task<bool> UpdatePermission(int id, Permission permission)
    {
        var existingPermission = await _context.Permissions.FindAsync(id);
        if (existingPermission == null) return false;

        existingPermission.Name = permission.Name;
        existingPermission.Description = permission.Description;
        _context.Permissions.Update(existingPermission);
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
