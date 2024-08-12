using ECommerce.API.Controllers.APIControllers;
using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.API.Controllers.Transactions
{
    [ApiController]
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        #region Constractor (s)
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        #endregion

        #region Create Order
        [HttpPost(Router.Order.Create)]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Core.Entities.OrderAggregate.Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new BaseGenericResult<dynamic>(400, "Problem creating order"));

            return Ok(order);
        }

        #endregion

        #region Get Orders For User
        [HttpGet(Router.Order.GetOrdersForUser)]
        public async Task<ActionResult> GetOrdersForUser()
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        #endregion

        #region Get Order By Id For User
        [HttpGet(Router.Order.GetOrderByIdForUser)]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null) return NotFound(new BaseGenericResult<dynamic>(404));
            return _mapper.Map<OrderToReturnDto>(order);
        }

        #endregion

        #region Delivery Method
        [HttpGet(Router.Order.DeliveryMethod)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }

        #endregion

    }
}
