using ECommerce.API.Controllers.APIControllers;
using ECommerce.Infrastrucure.Services.Permissions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ECommerce.Core.Identity;
using ECommerce.Core.DTOS;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ECommerce.API.Controllers.Auth
{
    [ApiController]
    public class AccountController : BaseAPIController
    {
        #region Constructor(s)

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly Mapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            Mapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Login

        [HttpPost(Router.Account.Login)]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login attempt failed: User with email {Email} not found", loginDto.Email);
                return Unauthorized(new BaseQueryResult(600, "User not found"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login attempt failed: Invalid credentials for user with email {Email}", loginDto.Email);
                return Unauthorized(new BaseQueryResult(401, "Invalid credentials"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return Ok(new UserDto
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email,
                EmailVerified = user.EmailConfirmed,
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = refreshToken,
                Roles = roles
            });
        }



        #endregion

        #region Register

        [HttpPost(Router.Account.Register)]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (await EmailExistsAsync(registerDto.Email))
            {
                _logger.LogWarning("Registration failed: Email {Email} is already in use", registerDto.Email);
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
                _logger.LogError("Registration failed for email {Email}. Errors: {Errors}", registerDto.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return BadRequest(new BaseQueryResult(400, "Problem registering user"));
            }

            await _userManager.AddToRoleAsync(user, "Regular");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), Router.Account.Prefix, new { userId = user.Id, token }, Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: {confirmationLink}");

            return Ok(new UserDto
            {
                Id = user.Id,
                Name = user.DisplayName,
                Email = user.Email,
                EmailVerified = user.EmailConfirmed,
                AccessToken = _tokenService.CreateToken(user),
                Roles = new List<string> { "Regular" }
            });
        }

        #endregion

        #region Refresh Token

        [HttpPost(Router.Account.RefreshToken)]
        public async Task<ActionResult<UserDto>> RefreshToken([FromBody] string refreshToken)
        {
            var user = await _userManager.Users.Include(u => u.RefreshTokens)
                .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken && t.IsActive));

            if (user == null)
            {
                _logger.LogWarning("Invalid refresh token attempt");
                return Unauthorized(new BaseQueryResult(401, "Invalid refresh token"));
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            return Ok(new UserDto
            {
                Email = user.Email,
                AccessToken = _tokenService.CreateToken(user),
                Name = user.DisplayName,
                RefreshToken = newRefreshToken
            });
        }

        #endregion

        #region Confirm Email

        [HttpGet(Router.Account.ConfirmEmail)]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Invalid email confirmation request: Missing userId or token");
                return BadRequest(new BaseQueryResult(400, "Invalid email confirmation request"));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Email confirmation failed: User not found with ID {UserId}", userId);
                return NotFound(new BaseQueryResult(404, "User not found"));
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                _logger.LogError("Email confirmation failed for user {UserId}. Errors: {Errors}", userId, string.Join(", ", result.Errors.Select(e => e.Description)));
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
                _logger.LogWarning("Failed to retrieve current user: User not found in claims");
                return Unauthorized(new BaseQueryResult(401, "User not found"));
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto
            {
                Email = user.Email,
                AccessToken = _tokenService.CreateToken(user),
                Name = user.DisplayName,
                Roles = roles
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
}
