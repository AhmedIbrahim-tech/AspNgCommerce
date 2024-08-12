using ECommerce.Core.Identity;

namespace ECommerce.API.Controllers;


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

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }

    #endregion

    #region Register

    [HttpPost(Router.Account.Register)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(new BaseQueryResult(400));

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
    }


    #endregion        [Authorize]

    [HttpPost(Router.Account.VerifyEmail)]
    public async Task<ActionResult> VerifyEmail(VerifyEmailDto verifyEmailDto)
    {
        if (string.IsNullOrEmpty(verifyEmailDto.Email) || string.IsNullOrEmpty(verifyEmailDto.Otp))
        {
            return BadRequest(new BaseQueryResult(400, "Email and OTP are required"));
        }

        var user = await _userManager.FindByEmailAsync(verifyEmailDto.Email);
        if (user == null)
        {
            return NotFound(new BaseQueryResult(404, "User not found"));
        }

        // Assume GetOtpDetailsAsync gets the OTP details for validation
        var otpDetails = await _tokenService.GetOtpDetailsAsync(verifyEmailDto.Email, verifyEmailDto.Otp);
        if (otpDetails == null || !otpDetails.IsValid)
        {
            return BadRequest(new BaseQueryResult(400, "Invalid OTP or OTP expired"));
        }

        var result = await _userManager.ConfirmEmailAsync(user, otpDetails.Token);
        if (!result.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Email verification failed"));
        }

        // Optionally, invalidate the OTP after successful verification
        await _tokenService.InvalidateOtpAsync(otpDetails);

        return Ok(new BaseQueryResult(200, "Email verified successfully"));
    }

    #endregion


    #region Get Current User

    [Authorize]
    [HttpGet(Router.Account.CurrentUser)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {

        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

        return new UserDto
        {
            return Unauthorized(new BaseQueryResult(401, "User not found"));
        }


        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
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
