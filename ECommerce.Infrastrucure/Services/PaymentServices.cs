using ECommerce.Core.Entities.OrderAggregate;
using Stripe;
using Order = ECommerce.Core.Entities.OrderAggregate.Order;

namespace ECommerce.Infrastrucure.Services;

public class PaymentServices : IPaymentServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _configuration;

    public PaymentServices(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId)
    {
        StripeConfiguration.ApiKey = _configuration["stripeSetting:Secretkey"];

        var basket = await _basketRepository.GetBasketAsync(BasketId);
        if (basket == null) return null;

        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItems = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(item.Id);
            if (item.Price != productItems.Price)
            {
                item.Price = productItems.Price;
            }
        }
        var service = new PaymentIntentService();
        PaymentIntent intent;
        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) +
                         (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) +
                         (long)shippingPrice * 100
            };

            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepository.UpdateBasketAsync(basket);
        return basket;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return null;
        order.Status = OrderStatus.PaymentFailed;
        await _unitOfWork.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateOrderPaymentSucceded(string paymentIntentId)
    {
        var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return null;
        order.Status = OrderStatus.PaymentReceived;
        await _unitOfWork.SaveChangesAsync();

        return order;
    }
}
