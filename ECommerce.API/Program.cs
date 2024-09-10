using ECommerce.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.AddCorsSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureMiddleware(app.Environment);

await DatabaseSetup.MigrateDatabaseAsync(app.Services);

app.Run();
