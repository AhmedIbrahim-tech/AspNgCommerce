using ECommerce.Core.Identity.Permission;
using ECommerce.Infrastrucure.Services.Permissions;

namespace ECommerce.API.Controllers;

[ApiController]
public class PermissionsController : ControllerBase
{
    #region Constructor
    private readonly IPermissionsService _permissionsService;

    public PermissionsController(IPermissionsService permissionsService)
    {
        _permissionsService = permissionsService;
    }
    #endregion

    #region Handle of Functions

    #region Get List of Permissions
    [HttpGet(Router.Permissions.GetAllPermissions)]
    public async Task<IActionResult> GetPermissions()
    {
        var result = await _permissionsService.GetAllPermissionsAsync();
        if (result.Status)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Get Single Permission
    [HttpGet(Router.Permissions.GetPermissionById)]
    public async Task<IActionResult> GetPermissionById(int id)
    {
        var result = await _permissionsService.GetPermissionByIdAsync(id);
        if (result.Status)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Create Permission
    [HttpPost(Router.Permissions.CreatePermission)]
    public async Task<IActionResult> CreatePermission([FromBody] PermissionDto permissionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _permissionsService.AddPermissionAsync(permissionDto);
        if (result.Status)
        {
            return StatusCode(201, result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Update Permission
    [HttpPut(Router.Permissions.UpdatePermission)]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] PermissionDto permissionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _permissionsService.UpdatePermissionAsync(id, permissionDto);
        if (result.Status)
        {
            return Ok(result);
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #region Delete Permission
    [HttpDelete(Router.Permissions.DeletePermission)]
    public async Task<IActionResult> DeletePermission(int id)
    {
        var result = await _permissionsService.DeletePermissionAsync(id);
        if (result.Status)
        {
            return StatusCode(204, "Deleted Successfully");
        }
        return StatusCode(result.StatusCode, result.Message);
    }
    #endregion

    #endregion
}

