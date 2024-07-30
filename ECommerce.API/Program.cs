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
   
});

#endregion

#region Dependency Injection

builder.Services.AddCoreDependencies().AddInfrastructureDependencies();
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

#region Error
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
#endregion

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

#region Update Database

using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
var context = Services.GetRequiredService<ApplicationDBContext>();


// Logger 
var logger = Services.GetRequiredService<ILogger<Program>>();

var loggerFactory = Services.GetRequiredService<ILoggerFactory>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "Error Occurred While Migrating Process");
}

#endregion

app.Run();
