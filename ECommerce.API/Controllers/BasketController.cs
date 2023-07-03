#region Fields
namespace ECommerce.API.Controllers
{
    [ApiController]
    public class BasketController : BaseAPIController
    {
        #endregion

        #region Contractor (s)
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
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
        public async Task<IActionResult> UpdatedBasket(CustomerBasket basket)
        {
            var result = await _basketRepository.UpdateBasketAsync(basket);
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
