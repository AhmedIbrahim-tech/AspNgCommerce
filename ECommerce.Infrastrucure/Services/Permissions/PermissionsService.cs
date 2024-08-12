using AutoMapper;
using ECommerce.Core.Identity.Permission;
using ECommerce.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastrucure.Services.Permissions;

#region Interface
public interface IPermissionsService
{
    Task<BaseGenericResult<List<PermissionDto>>> GetAllPermissionsAsync();
    Task<BaseGenericResult<PermissionDto>> GetPermissionByIdAsync(int id);
    Task<BaseGenericResult<PermissionDto>> AddPermissionAsync(PermissionDto permissionDto);
    Task<BaseGenericResult<PermissionDto>> UpdatePermissionAsync(int id, PermissionDto permissionDto);
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

    public async Task<BaseGenericResult<List<PermissionDto>>> GetAllPermissionsAsync()
    {
        var permissionDtos = await _unitOfWork.PermissionsRepository.GetAllPermissionsAsync();
        return new BaseGenericResult<List<PermissionDto>>(true, (int)HttpStatusCode.OK, "Permissions fetched successfully", permissionDtos.ToList());
    }

    public async Task<BaseGenericResult<PermissionDto>> GetPermissionByIdAsync(int id)
    {
        var permissionDto = await _unitOfWork.PermissionsRepository.GetPermissionByIdAsync(id);
        if (permissionDto == null)
            return new BaseGenericResult<PermissionDto>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<PermissionDto>(true, (int)HttpStatusCode.OK, "Permission fetched successfully", permissionDto);
    }

    public async Task<BaseGenericResult<PermissionDto>> AddPermissionAsync(PermissionDto permissionDto)
    {
        _unitOfWork.PermissionsRepository.AddPermission(permissionDto);
        await _unitOfWork.SaveChangesAsync();

        return new BaseGenericResult<PermissionDto>(true, (int)HttpStatusCode.Created, "Permission added successfully", permissionDto);
    }

    public async Task<BaseGenericResult<PermissionDto>> UpdatePermissionAsync(int id, PermissionDto permissionDto)
    {
        var success = await _unitOfWork.PermissionsRepository.UpdatePermission(id, permissionDto);
        if (!success)
            return new BaseGenericResult<PermissionDto>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<PermissionDto>(true, (int)HttpStatusCode.OK, "Permission updated successfully", permissionDto);
    }

    public async Task<BaseGenericResult<int>> DeletePermissionAsync(int id)
    {
        var success = await _unitOfWork.PermissionsRepository.DeletePermission(id);
        if (!success)
            return new BaseGenericResult<int>(false, (int)HttpStatusCode.NotFound, "Permission not found");

        return new BaseGenericResult<int>(true, (int)HttpStatusCode.OK, "Permission deleted successfully");
    }
}