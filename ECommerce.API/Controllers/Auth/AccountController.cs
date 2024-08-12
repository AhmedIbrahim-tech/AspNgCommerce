using ECommerce.API.Controllers.APIControllers;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ECommerce.API.Controllers.Auth;


[ApiController]
public class AccountController : BaseAPIController
{
    #region Constructor(s)

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IMapper mapper,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
        _emailSender = emailSender;
    }

    #endregion

    #region Login

    [HttpPost(Router.Account.Login)]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized(new BaseQueryResult(600, "User not found"));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new BaseQueryResult(401, "Invalid credentials"));
        }

        return Ok(new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        });
    }

    #endregion

    #region Register

    [HttpPost(Router.Account.Register)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (await EmailExistsAsync(registerDto.Email))
        {
            return BadRequest(new BaseQueryResult(400, "Email address is already in use"));
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Problem registering user"));
        }

        // Generate email confirmation token
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // Send email with confirmation link
        var confirmationLink = Url.Action(nameof(ConfirmEmail), Router.Account.Prefix, new { userId = user.Id, token }, Request.Scheme);
        await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: {confirmationLink}");

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        });
    }

    #endregion

    #region Refresh Token

    [HttpPost(Router.Account.RefreshToken)]
    public async Task<ActionResult<UserDto>> RefreshToken([FromBody] string refreshToken)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));

        if (user == null) return Unauthorized(new BaseQueryResult(401, "Invalid refresh token"));

        var existingToken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken);

        if (existingToken == null || !existingToken.IsActive) return Unauthorized(new BaseQueryResult(401, "Invalid refresh token"));

        // Invalidate the old refresh token and issue a new one
        existingToken.Revoked = DateTime.UtcNow;

        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        return Ok(new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName,
            RefreshToken = newRefreshToken.Token
        });
    }

    #endregion

    #region Confirm Email

    [HttpGet(Router.Account.ConfirmEmail)]
    public async Task<ActionResult> ConfirmEmail(string userId, string token)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            return BadRequest(new BaseQueryResult(400, "Invalid email confirmation request"));
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound(new BaseQueryResult(404, "User not found"));
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Email confirmation failed"));
        }

        return Ok(new BaseQueryResult(200, "Email confirmed successfully"));
    }

    #endregion

    #region Get Current User

    [Authorize]
    [HttpGet(Router.Account.CurrentUser)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

        if (user == null)
        {
            return Unauthorized(new BaseQueryResult(401, "User not found"));
        }

        return Ok(new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        });
    }

    #endregion

    #region Check Email Exists

    [HttpGet(Router.Account.EmailExists)]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await EmailExistsAsync(email);
    }

    private async Task<bool> EmailExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    #endregion

    #region Address

    [Authorize]
    [HttpGet(Router.Account.InitializationAddress)]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        if (user?.Address == null)
        {
            return NotFound(new BaseQueryResult(404, "Address not found"));
        }

        return Ok(_mapper.Map<AddressDto>(user.Address));
    }

    [Authorize]
    [HttpPut(Router.Account.UpdateAddress)]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto addressDto)
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        if (user == null)
        {
            return Unauthorized(new BaseQueryResult(401, "User not found"));
        }

        user.Address = _mapper.Map<Address>(addressDto);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Problem updating the user address"));
        }

        return Ok(_mapper.Map<AddressDto>(user.Address));
    }

    #endregion
}
