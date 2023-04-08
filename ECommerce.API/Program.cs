using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Connection Database

builder.Services.AddDbContext<ApplicationDBContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

#endregion

#region Add Fluent Validation

//builder.Services
//    .AddMvc(options => options.Filters.Add<ValidationFilter>())
//    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
