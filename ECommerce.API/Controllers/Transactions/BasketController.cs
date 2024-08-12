#region Fields
using ECommerce.API.Controllers.APIControllers;

namespace ECommerce.API.Controllers.Transactions
{
    [ApiController]
    public class BasketController : BaseAPIController
    {
        #endregion

        #region Contractor (s)
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        #endregion

        #region Get Basket
        [HttpGet(Router.Basket.GetBasket)]
        public async Task<IActionResult> GetBasket(string id)
        {
            var result = await _basketRepository.GetBasketAsync(id);
            return Ok(result ?? new CustomerBasket(id));
        }
        #endregion

        #region Update Basket
        [HttpPost(Router.Basket.UpdateBasket)]
        public async Task<IActionResult> UpdatedBasket(CustomerBasketDto basket)
        {
            var mapper = _mapper.Map<CustomerBasket>(basket);
            var result = await _basketRepository.UpdateBasketAsync(mapper);
            return Ok(result);
        }
        #endregion

        #region Delete Basket
        [HttpDelete(Router.Basket.DeleteBasket)]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            var result = await _basketRepository.DeleteBasketAsync(id);
            return Ok(result);
        }

        #endregion
    }
}
