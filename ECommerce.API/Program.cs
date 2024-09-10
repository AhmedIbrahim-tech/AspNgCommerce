using ECommerce.Infrastrucure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Model State Invalid

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});

#endregion

builder.Services.AddEndpointsApiExplorer();

#region Configure Swagger Doc

builder.Services.AddSwaggerGen(options =>
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

#endregion

#region Configure database and identity


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddSignInManager<SignInManager<AppUser>>();
#endregion

#region Configure JWT authentication

var jwt = builder.Configuration.GetSection("Token");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

builder.Services.AddAuthorization();

#endregion

#region Configure MemoryCache

builder.Services.AddMemoryCache();

#endregion

#region Configure DI

builder.Services.AddCoreDependencies().AddInfrastructureDependencies();
builder.Services.AddScoped<ITokenService, TokenService>();

#endregion

#region Redis
//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

//builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
//{
//    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
//    return ConnectionMultiplexer.Connect(options);
//});

#endregion

#region CORS Support

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); //.WithOrigins("http://localhost:4200");
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // It's Important To Add Images

#region CORS Support
app.UseCors("CorsPolicy");
#endregion

#region Auth
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
#endregion

app.MapControllers();

#region Error
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
#endregion





#region Update Database

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<ApplicationDBContext>();


// Logger 
var logger = Services.GetRequiredService<ILogger<Program>>();

var loggerFactory = Services.GetRequiredService<ILoggerFactory>();
var userManager = Services.GetRequiredService<UserManager<AppUser>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await UserIdentitySeed.SeedUserAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error Occurred While Migrating Process");
}

#endregion

app.Run();
