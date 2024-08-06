namespace ECommerce.API.Controllers;

[ApiController]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    #region Get All Roles

    [HttpGet(Router.Roles.GetAll)]
    public ActionResult<IEnumerable<RoleDto>> GetAllRoles()
    {
        var roles = _roleManager.Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList();
        return Ok(roles);
    }

    #endregion

    #region Get Role By ID

    [HttpGet(Router.Roles.GetById)]
    public async Task<ActionResult<RoleDto>> GetRoleById(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound(new BaseQueryResult(404, "Role not found"));
        }

        return Ok(new RoleDto { Id = role.Id, Name = role.Name });
    }

    #endregion

    #region Create Role

    [HttpPost(Router.Roles.Create)]
    public async Task<ActionResult<RoleDto>> CreateRole([FromBody] RoleDto roleDto)
    {
        var role = new IdentityRole(roleDto.Name);
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Failed to create role"));
        }

        return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, new RoleDto { Id = role.Id, Name = role.Name });
    }

    #endregion

    #region Update Role

    [HttpPut(Router.Roles.Update)]
    public async Task<ActionResult> UpdateRole(string id, [FromBody] RoleDto roleDto)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound(new BaseQueryResult(404, "Role not found"));
        }

        role.Name = roleDto.Name;
        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Failed to update role"));
        }

        return NoContent();
    }

    #endregion

    #region Delete Role

    [HttpDelete(Router.Roles.Delete)]
    public async Task<ActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound(new BaseQueryResult(404, "Role not found"));
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Failed to delete role"));
        }

        return NoContent();
    }

    #endregion

    #region Assign Role to User

    [HttpPost(Router.Roles.Assign)]
    public async Task<ActionResult> AssignRoleToUser([FromBody] AssignRoleDto assignRoleDto)
    {
        var user = await _userManager.FindByEmailAsync(assignRoleDto.Email);
        if (user == null)
        {
            return NotFound(new BaseQueryResult(404, "User not found"));
        }

        var result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Failed to assign role to user"));
        }

        return Ok(new BaseQueryResult(200, "Role assigned to user successfully"));
    }

    #endregion

    #region Remove Role from User

    [HttpPost(Router.Roles.Remove)]
    public async Task<ActionResult> RemoveRoleFromUser([FromBody] AssignRoleDto assignRoleDto)
    {
        var user = await _userManager.FindByEmailAsync(assignRoleDto.Email);
        if (user == null)
        {
            return NotFound(new BaseQueryResult(404, "User not found"));
        }

        var result = await _userManager.RemoveFromRoleAsync(user, assignRoleDto.RoleName);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Failed to remove role from user"));
        }

        return Ok(new BaseQueryResult(200, "Role removed from user successfully"));
    }

    #endregion

}