
namespace ECommerce.Infrastrucure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        #region Dependency Injection
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        //Repository
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductsRepository, ProductsRepository>();
        services.AddTransient<IProductsServices, ProductsServices>();

        //Services
        services.AddTransient<IProductServices, ProductServices>();



        #endregion

        #region Add Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        #endregion

        return services;
    }

}
