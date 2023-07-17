using ECommerce.Core.Identity;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

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
builder.Services.AddSwaggerGen();


#region Connection Database

builder.Services.AddDbContext<ApplicationDBContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

#endregion

#region Swagger Doc

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ECommerce"
    });


    //var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
    //options.IncludeXmlComments(xmlpath);
});

#endregion

#region Dependency Injection

builder.Services.AddCoreDependencies().AddInfrastructureDependencies();

//Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});
#endregion

#region CORS Support

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"); //AllowAnyOrigin
    });
});

#endregion

#region Authentication

builder.Services.AddIdentityCore<AppUser>(options =>
{

})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Error
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
#endregion

app.UseHttpsRedirection();

app.UseStaticFiles(); // It's Important To Add Images

#region CORS Support
app.UseCors("CorsPolicy");
#endregion

#region Authentication
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.MapControllers();

#region Update Database

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<ApplicationDBContext>();

// Identity
var usermanager = Services.GetRequiredService<UserManager<AppUser>>();
var logger = Services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUserAsync(usermanager);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error Occurred While Migrating Process");
}

#endregion

app.Run();
