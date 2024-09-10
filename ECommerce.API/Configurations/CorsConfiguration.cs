namespace ECommerce.API.Configurations;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsSupport(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); //.WithOrigins("http://localhost:4200");
            });
        });

        return services;
    }
}
