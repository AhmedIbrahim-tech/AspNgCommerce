using ECommerce.Infrastrucure.Services;
using ECommerce.Infrastrucure.Services.Permissions;

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
        services.AddTransient<IProductBrandRepository, ProductBrandRepository>();
        services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
        services.AddTransient<IProductsServices, ProductsServices>();
        services.AddTransient<IBasketRepository, BasketRepository>();

        //Services
        services.AddTransient<IProductServices, ProductServices>();
        services.AddTransient<IProductBrandService, ProductBrandService>();
        services.AddTransient<IProductTypeService, ProductTypeService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPaymentServices, PaymentServices>();
        services.AddTransient<IPermissionsService, PermissionsService>();



        #endregion

        #region Add Fluent Validation

        //builder.Services
        //    .AddMvc(options => options.Filters.Add<ValidationFilter>())
        //    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        //services.AddFluentValidation(options =>
        //{
        //    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        //});

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        #endregion

        return services;
    }

}
