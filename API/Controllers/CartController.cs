using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController : BaseApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartByIdAsync(string id)
        {
            var cart = await _cartRepository.GetCartAsync(id);
            return Ok(cart ?? new Cart(id));
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCartAsync(CartDto cart)
        {
            var customerCart = _mapper.Map<CartDto, Cart>(cart);

            var updatedCart = await _cartRepository.UpdateCartAsync(customerCart);

            return Ok(updatedCart);
        }

        [HttpDelete]

        public async Task DeleteCartAsync(string id)
        {
            await _cartRepository.DeleteCardAsync(id);

        }
    }
}