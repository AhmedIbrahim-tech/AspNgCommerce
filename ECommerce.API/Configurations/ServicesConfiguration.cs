using ECommerce.Infrastrucure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.API.Configurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        });

        services.AddEndpointsApiExplorer();

        // Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ECommerce API"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
        });

        // Database and Identity
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddSignInManager<SignInManager<AppUser>>();

        // JWT Authentication
        var jwt = configuration.GetSection("Token");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])),
                    ValidIssuer = jwt["Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization();

        // MemoryCache
        services.AddMemoryCache();

        // Dependency Injection
        services.AddCoreDependencies().AddInfrastructureDependencies();
        services.AddScoped<ITokenService, TokenService>();

        // Redis (Optional)
        //services.AddSingleton<IConnectionMultiplexer>(c =>
        //{
        //    var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
        //    return ConnectionMultiplexer.Connect(options);
        //});

        return services;
    }
}
