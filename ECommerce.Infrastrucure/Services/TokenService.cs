using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;

namespace ECommerce.Infrastrucure.Services;


public interface ITokenService
{
    string CreateToken(AppUser user);
    RefreshToken GenerateRefreshToken();
    Task<OtpDetails> GetOtpDetailsAsync(string email, string otp);
    Task InvalidateOtpAsync(OtpDetails otpDetails);
    Task SaveOtpDetailsAsync(OtpDetails otpDetails);
}


public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly ApplicationDBContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config, ApplicationDBContext context, IMemoryCache memoryCache)
    {
        _config = config;
        _context = context;
        _memoryCache = memoryCache;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:key"]));
    }
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Token:Issuer"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }



    public RefreshToken GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };
    }


    public async Task<OtpDetails> GetOtpDetailsAsync(string email, string otp)
    {
            var otpDetails = await _context.OtpDetails
                .FirstOrDefaultAsync(od => od.Email == email && od.Otp == otp && od.ExpiresAt > DateTime.UtcNow && od.IsValid);

            return otpDetails;
    }

    public async Task InvalidateOtpAsync(OtpDetails otpDetails)
    {
            otpDetails.IsValid = false;
            _context.Update(otpDetails);
            await _context.SaveChangesAsync();
    }



    public async Task SaveOtpDetailsAsync(OtpDetails otpDetails)
    {
        // Use an async method to simulate database or cache insertion
        await Task.Run(() =>
        {
            _memoryCache.Set(otpDetails.Email, otpDetails, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15) // Set the expiration time
            });
        });
    }


}

