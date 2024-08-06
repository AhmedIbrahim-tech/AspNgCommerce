
namespace ECommerce.Core.Mappings;

public class URLResolver : IValueResolver<Product, ProductDto, string>
{
    private readonly IConfiguration _configuration;
    public URLResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Product source, ProductDto destination, string estMember, ResolutionContext context)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (!string.IsNullOrEmpty(source.PictureURL))
        {
            return _configuration["APIURL"] + source.PictureURL;
        }
        return null;
    }
}
