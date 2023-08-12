using ECommerce.Core.Identity;

namespace ECommerce.API.Controllers;

[ApiController]
public class AccountController : BaseAPIController
{
    #region Constructor (s)
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    #endregion

    #region Login
    [HttpPost(Router.Account.Login)]
    public async Task<ActionResult<UserDto>> login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new BaseQueryResult(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized(new BaseQueryResult(401));

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName
        };
    }
    #endregion

    #region Register
    [HttpPost(Router.Account.Register)]
    public async Task<ActionResult<UserDto>> Regitser(RegisterDto registerDto)
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

    #region Get Current User
    [HttpGet(Router.Account.CurrentUser)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {

        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

        return new UserDto
        {
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
        return await _userManager.FindByEmailAsync(email) != null;
    }

    #endregion

    #region Address
    [Authorize]
    [HttpGet(Router.Account.InitializationAddress)]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {

        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        return _mapper.Map<Address, AddressDto>(user.Address);

    }


    [Authorize]
    [HttpPut(Router.Account.UpdateAddress)]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
    {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        user.Address = _mapper.Map<AddressDto, Address>(address);

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));
        return BadRequest("Problem updating the user");
    }
    #endregion

}
