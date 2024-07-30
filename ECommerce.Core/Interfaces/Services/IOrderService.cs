using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Entities.OrderAggregate.Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byuyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

}
