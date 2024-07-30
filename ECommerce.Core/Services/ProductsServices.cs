using ECommerce.Core.Interfaces.UnitOfWork;

namespace ECommerce.Core.Services;

public class ProductsServices : IProductsServices
{
    #region Contractor

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsServices(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #endregion

    #region Get List Of Products

    public async Task<BaseGenericResult<PagedList<Product>>> GetListOfProductsAsync(ProductsQueryFilter filter)
    {
        try
        {
            var result = await _unitOfWork.ProductsRepository.GetListOfProductsAsync();

            #region Filtration

            filter.PageNumber = filter.PageNumber == 0 ? 1 : filter.PageNumber;

            filter.PageSize = filter.PageSize == 0 ? 10 : filter.PageSize;

            if (filter.BrandId != null)
            {
                result = result.Where(x => x.ProductBrandID == filter.BrandId);
            }

            if (filter.TypeId != null)
            {
                result = result.Where(x => x.ProductTypeID == filter.TypeId);
            }

            if (filter.Name != null)
            {
                result = result.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.Search != null)
            {
                result = result.Where(x =>
                   x.Description.ToLower().Contains(filter.Search.ToLower())
                || x.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            #endregion

            #region Sorting

            switch (filter.Sort)
            {
                case "priceAsc":
                    result = result.OrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    result = result.OrderByDescending(x => x.Price);
                    break;
                default:
                    result = result.OrderBy(x => x.Name);
                    break;
            }

            #endregion


            var pagedProducts = PagedList<Product>.Create(result, filter.PageNumber, filter.PageSize);



            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", pagedProducts);

        }
        catch (Exception ex)
        {

            return new(false, (int)HttpStatusCode.InternalServerError, "Data Loading Failed → " + ex.Message);

        }
    }

    #endregion

    #region Get Single Of Products

    public async Task<BaseGenericResult<Product>> GetProductByIDAsync(int id)
    {
        try
        {
            var result = await _unitOfWork.ProductsRepository.GetProductByIDAsync(id);
            return new(true, (int)HttpStatusCode.OK, "Data Loading Successfully", result);

        }
        catch (Exception ex)
        {

            return new(false, (int)HttpStatusCode.InternalServerError, "Data Loading Failed → " + ex.Message);

        }
    }

    #endregion


}
