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
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, OrderAddress shippingAddress)
        {
            // get cart from the repo
            var cart = await _cartRepository.GetCartAsync(cartId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.NameAr, productItem.NameEn, productItem.ImgUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Qty);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Qty);

            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            //TODO: save to db

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // delete cart 
            await _cartRepository.DeleteCardAsync(cartId);
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