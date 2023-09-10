using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.Core.Interfaces.Services;

public interface IPaymentServices
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
    Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    Task<Order> UpdateOrderPaymentSucceded(string paymentIntentId);
}
