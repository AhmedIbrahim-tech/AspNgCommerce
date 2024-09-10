using ECommerce.Core.Identity.Authorization;
using ECommerce.Core.Responses;
using System.Net;

namespace ECommerce.Infrastrucure.Services.Permissions;

#region Interface
public interface IPermissionsService
{
    Task<BaseGenericResult<List<Permission>>> GetAllPermissionsAsync();
    Task<BaseGenericResult<Permission>> GetPermissionByIdAsync(int id);
    Task<BaseGenericResult<Permission>> AddPermissionAsync(Permission permission);
    Task<BaseGenericResult<Permission>> UpdatePermissionAsync(int id, Permission permission);
    Task<BaseGenericResult<int>> DeletePermissionAsync(int id);
}
#endregion

public class PermissionsService : IPermissionsService
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseGenericResult<List<Permission>>> GetAllPermissionsAsync()
    {
        var permissions = await _unitOfWork.PermissionsRepository.GetAllPermissionsAsync();
        return new BaseGenericResult<List<Permission>>(true, (int)HttpStatusCode.OK, "Permissions fetched successfully", permissions.ToList());
    }

    public async Task<BaseGenericResult<Permission>> GetPermissionByIdAsync(int id)
    {
        var permission = await _unitOfWork.PermissionsRepository.GetPermissionByIdAsync(id);
        if (permission == null)
            return new BaseGenericResult<Permission>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<Permission>(true, (int)HttpStatusCode.OK, "Permission fetched successfully", permission);
    }

    public async Task<BaseGenericResult<Permission>> AddPermissionAsync(Permission permission)
    {
        _unitOfWork.PermissionsRepository.AddPermission(permission);
        await _unitOfWork.SaveChangesAsync();

        return new BaseGenericResult<Permission>(true, (int)HttpStatusCode.Created, "Permission added successfully", permission);
    }

    public async Task<BaseGenericResult<Permission>> UpdatePermissionAsync(int id, Permission permission)
    {
        var success = await _unitOfWork.PermissionsRepository.UpdatePermission(id, permission);
        if (!success)
            return new BaseGenericResult<Permission>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<Permission>(true, (int)HttpStatusCode.OK, "Permission updated successfully", permission);
    }

    public async Task<BaseGenericResult<int>> DeletePermissionAsync(int id)
    {
        var success = await _unitOfWork.PermissionsRepository.DeletePermission(id);
        if (!success)
            return new BaseGenericResult<int>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<int>(true, (int)HttpStatusCode.OK, "Permission deleted successfully");
    }

}