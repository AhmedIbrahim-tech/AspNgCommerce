using ECommerce.Core.Entities.OrderAggregate;
using Address = ECommerce.Core.Entities.OrderAggregate.Address;

namespace ECommerce.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byuyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

}
