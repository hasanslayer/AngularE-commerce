using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        public Task<bool> DeleteCardAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}