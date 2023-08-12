using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.Infrastrucure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethods;
        private readonly IGenericRepository<Core.Entities.OrderAggregate.Order> _OrderRepository;

        public OrderService(

            IUnitOfWork unitOfWork,
            IBasketRepository basketRepo,
            IGenericRepository<DeliveryMethod> deliveryMethods,
            IGenericRepository<Core.Entities.OrderAggregate.Order> OrderRepository
            )
        {
            _basketRepo = basketRepo;
            _deliveryMethods = deliveryMethods;
            _OrderRepository = OrderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Core.Entities.OrderAggregate.Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Core.Entities.OrderAggregate.Address shippingAddress)
        {
            //get the basket from basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            //get the items from product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.ProductRepository.GetProductByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //get delivery method from the repo
            var deliveryMethod = await _deliveryMethods.GetByIdAsync(deliveryMethodId);

            //clac subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            //create the order
            var order = new Core.Entities.OrderAggregate.Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            await _OrderRepository.AddAsync(order);

            //save to db
            var result = await _unitOfWork.SaveChangesAsync();

            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);


            //return order
            if (result <= 0) return null;
            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _deliveryMethods.ListAllAsync();
        }

        public async Task<Core.Entities.OrderAggregate.Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _OrderRepository.GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Core.Entities.OrderAggregate.Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

            return await _OrderRepository.ListAsync(spec);
        }


        //public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        //{



        //    var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
        //    var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        //    if (order != null)
        //    {
        //        order.ShipToAddress = shippingAddress;
        //        order.DeliveryMethod = deliveryMethod;
        //        order.Subtotal = subtotal;
        //        _unitOfWork.Repository<Order>().Update(order);
        //    }
        //    else
        //    {
        //        order = new Order(items, buyerEmail, shippingAddress, deliveryMethod,
        //            subtotal, basket.PaymentIntentId);
        //        _unitOfWork.Repository<Order>().Add(order);
        //    }
        //    //create the order


        //    //TODO:save to db
        //    var result = await _unitOfWork.Complete();

        //    //delete basket
        //    // await _basketRepo.DeleteBasketAsync(basketId);
        //    //return order

        //    if (result <= 0) return null;
        //    return order;
        //}

    }
}
