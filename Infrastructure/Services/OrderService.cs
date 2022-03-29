using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<DeliveryMethod> _dbRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly ICartRepository _cartRepository;
        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dbRepo, IGenericRepository<Product> productRepo, ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _productRepo = productRepo;
            _dbRepo = dbRepo;
            _orderRepo = orderRepo;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, OrderAddress shippingAddress)
        {
            // get cart from the repo
            var cart = await _cartRepository.GetCartAsync(cartId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.NameAr, productItem.NameEn, productItem.ImgUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Qty);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _dbRepo.GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Qty);

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            //TODO: save to db

            // return order
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}