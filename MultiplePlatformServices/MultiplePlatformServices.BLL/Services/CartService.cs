using Mapster;
using Microsoft.AspNetCore.Http;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class CartService : ICartService
    {

        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;


        public CartService(
            ICartRepository cartRepository,
            IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository
            )
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }


        private string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;
        }

        public async Task<CartResponse?> AddToCartAsync(CartRequest request)
        {
            // Get current logged-in user
            var userId = GetUserId();

            if (userId == null)
                return null;

            // Validate quantity
            if (request.Quantity <= 0)
                return null;

            // Check if product exists
            var product = await _productRepository.GetOne(
                p => p.Id == request.ProductId
            );

            if (product == null)
                return null;

            // Check if product is active
            if (!product.IsActive)
                return null;

            // Check stock
            if (request.Quantity > product.StockQuantity)
                return null;

            // Get user's cart
            var cart = await _cartRepository.GetOne(
                c => c.UserId == userId,
                includes:
                [
                    "CartItems.Product"
                ]
            );

            // If user doesn't have a cart, create one
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    UpdatedAt = DateTime.UtcNow
                };

                cart = await _cartRepository.CreateAsync(cart);
            }

            // Check if product already exists in cart
            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.ProductId == request.ProductId);

            if (cartItem != null)
            {
                // New total quantity
                var newQuantity = cartItem.Quantity + request.Quantity;

                // Check stock again
                if (newQuantity > product.StockQuantity)
                    return null;

                cartItem.Quantity = newQuantity;

                // Keep current price or update it
                cartItem.UnitPrice = product.Price;

                cart.UpdatedAt = DateTime.UtcNow;

                await _cartRepository.UpdateAsync(cart);
            }
            else
            {
                // Add new item
                var newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price
                };

                cart.CartItems.Add(newCartItem);

                cart.UpdatedAt = DateTime.UtcNow;

                await _cartRepository.UpdateAsync(cart);
            }

            // Get updated cart
            var updatedCart = await _cartRepository.GetOne(
                c => c.Id == cart.Id,
                includes:
                [
                    "CartItems.Product"
                ]
            );

            if (updatedCart == null)
                return null;

            return updatedCart.Adapt<CartResponse>();
        }



        public async Task<CartResponse?> GetCartAsync()
        {
            var userId = _httpContextAccessor.HttpContext?
        .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return null;

            var cart = await _cartRepository.GetOne(
                c => c.UserId == userId,
                includes:
                [
                    "CartItems.Product"
                ]
            );

            if (cart == null)
                return null;

            return cart.Adapt<CartResponse>();
        }




        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            var cart = await _cartRepository.GetOne(
                c => c.UserId == userId,
                includes:
                [
                    "CartItems"
                ]
            );

            if (cart == null)
                return false;

            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id == id);

            if (cartItem == null)
                return false;

            cart.CartItems.Remove(cartItem);

            cart.UpdatedAt = DateTime.UtcNow;

            return await _cartRepository.UpdateAsync(cart);
        }


        // CLEAR CART
        public async Task<bool> ClearCartAsync()
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            var cart = await _cartRepository.GetOne(
                c => c.UserId == userId,
                includes:
                [
                    "CartItems"
                ]
            );

            if (cart == null)
                return false;

            cart.CartItems.Clear();

            cart.UpdatedAt = DateTime.UtcNow;

            return await _cartRepository.UpdateAsync(cart);
        }

        public async Task<bool> UpdateQuantityAsync(int id, int quantity)
        {
            if (quantity <= 0)
                return false;

            var userId = GetUserId();

            if (userId == null)
                return false;

            var cart = await _cartRepository.GetOne(
                c => c.UserId == userId,
                includes:
                [
                    "CartItems.Product"
                ]
            );

            if (cart == null)
                return false;

            var cartItem = cart.CartItems
                .FirstOrDefault(x => x.Id == id);

            if (cartItem == null)
                return false;

            cartItem.Quantity = quantity;

            cart.UpdatedAt = DateTime.UtcNow;

            return await _cartRepository.UpdateAsync(cart);
        }
    }
}
