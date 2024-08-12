using ECommerce.Infrastrucure.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

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
            EmailVerified = user.EmailConfirmed,
            AccessToken = token,
            RefreshToken = refreshToken,
            Roles = roles
        };

        return Ok(userDto);

    }

    #endregion

    #region Register

    [HttpPost(Router.Account.Register)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
        {
            return BadRequest(new { message = "Email address is already in use" });
        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!createUserResult.Succeeded)
        {
            return BadRequest(new BaseQueryResult(400, "Problem registering user"));
        }

        // Assigning a role to the user
        var roleName = "Regular"; // Default role or based on some logic
        var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!addToRoleResult.Succeeded)
        {
            return BadRequest(addToRoleResult.Errors);
        }


        // Generate email confirmation token
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
        await _emailSender.SendEmailAsync(registerDto.Email, "Confirm your registration",
            $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

        var tokenString = _tokenService.CreateToken(user); // Simplified token creation

        return Ok(new UserResponseDto
        {
            //Id = user.Id,
            Email = user.Email,
            Name = user.DisplayName,
            EmailVerified = user.EmailConfirmed,
            Token = tokenString,
            Roles = new List<RoleDto> { new RoleDto { Name = roleName } }
        });

    }

    #endregion

    #region Refresh Token

    [HttpPost(Router.Account.RefreshToken)]
    public async Task<ActionResult<UserDto>> RefreshToken([FromBody] string refreshToken)
    {

        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("Refresh token is required");
        }

        // Find the user by the refresh token
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

        var userDto = new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            EmailVerified = user.EmailConfirmed,
            AccessToken = _tokenService.CreateToken(user),
            RefreshToken = newRefreshToken,
        };


        return Ok(userDto);
    }

    #endregion

    #region Confirm Email

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

        if (user == null)
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
            EmailVerified = user.EmailConfirmed,
            AccessToken = token,
            RefreshToken = refreshToken,
            Roles = roles
        };

        return Ok(userDto);
    }

    #endregion

    #region Forget password
    [HttpPost(Router.Account.forgetpassword)]
    public async Task<IActionResult> ForgetPassword([FromBody] EmailCheckDto forgetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
        if (user == null)
        {
            return NotFound(new { message = "User with the given email does not exist." });
        }

        // Generate OTP
        var otp = GenerateOtp();

        // Store the OTP details in the database or cache
        var otpDetails = new OtpDetails
        {
            Email = forgetPasswordDto.Email,
            Otp = otp,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15) // OTP expires after 15 minutes
        };
        await _tokenService.SaveOtpDetailsAsync(otpDetails);

        // Send the OTP via email
        await _emailSender.SendEmailAsync(forgetPasswordDto.Email, "Password Reset OTP", $"Your OTP for password reset is: {otp}");

        return Ok(new { success = true, message = "OTP has been sent to your email." });
    }

    private string GenerateOtp()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString(); // Generates a 6-digit OTP
    } 
    #endregion

    #region is User Email Found
    [HttpGet(Router.Account.isUserEmailFound)]
    public async Task<IActionResult> IsUserEmailFound([FromBody] EmailCheckDto emailCheck)
    {
        var user = await _userManager.FindByEmailAsync(emailCheck.Email);
        bool isUserFound = user != null;
        return Ok(new { IsUserFound = isUserFound });
    }
    #endregion

    #region Resend Otp
    [HttpPost(Router.Account.resendotp)]
    public async Task<IActionResult> ResendOtp([FromBody] EmailCheckDto request)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Email is required.");
        }

        // Verify if the user exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Generate a new OTP
        var otp = GenerateOtp();

        // Save OTP details
        var otpDetails = new OtpDetails
        {
            Email = request.Email,
            Otp = otp,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15)  // OTP expires in 15 minutes
        };

        await _tokenService.SaveOtpDetailsAsync(otpDetails);

        // Send OTP via email
        await _emailSender.SendEmailAsync(request.Email, "Your OTP", $"Your OTP is: {otp}");

        return Ok(new { success = true, message = "OTP resent successfully." });
    }
    #endregion

    #region Retrieve Password
    [HttpPost(Router.Account.RetrievePassword)]
    public async Task<IActionResult> RetrievePassword([FromBody] RetrievePasswordRequestDto request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Otp))
        {
            return BadRequest("Email, new password, and OTP are required.");
        }

        // Verify if the user exists
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Verify the OTP
        var otpDetails = await _tokenService.GetOtpDetailsAsync(request.Email, request.Otp);
        if (otpDetails == null || !otpDetails.IsValid || otpDetails.ExpiresAt < DateTime.UtcNow)
        {
            return BadRequest("Invalid or expired OTP.");
        }

        // Update the password
        var resetPassResult = await _userManager.ResetPasswordAsync(user, otpDetails.Token, request.Password);
        if (!resetPassResult.Succeeded)
        {
            return BadRequest("Error resetting password.");
        }

        // Optionally invalidate the OTP after successful usage
        await _tokenService.InvalidateOtpAsync(otpDetails);

        return Ok(new { success = true, message = "Password updated successfully." });
    }
    #endregion

    #region Change Password
    [HttpPost(Router.Account.ChangePassword)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated");
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound("User not found");
        }

        // Check if the old password is correct
        var checkOldPassword = await _userManager.CheckPasswordAsync(user, changePasswordDto.OldPassword);
        if (!checkOldPassword)
        {
            return BadRequest("Incorrect old password");
        }

        // Change the password
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "Password changed successfully" });
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
