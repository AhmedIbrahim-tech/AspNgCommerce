using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Core;

public static class ModuleCoreDependencies
{
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
    {
        #region Auto Mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        return services;
    }
}
