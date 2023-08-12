using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpPost(Router.Order.Create)]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Core.Entities.OrderAggregate.Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new BaseGenericResult<dynamic>(400, "Problem creating order"));

            return Ok(order);
        }


        [HttpGet(Router.Order.GetOrdersForUser)]
        public async Task<ActionResult> GetOrdersForUser()
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet(Router.Order.GetOrderByIdForUser)]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null) return NotFound(new BaseGenericResult<dynamic>(404));
            return _mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet(Router.Order.DeliveryMethod)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }


    }
}
