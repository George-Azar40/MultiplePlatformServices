using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync();

            if (cart == null)
                return NotFound();

            return Ok(cart);
        }


        [HttpPost("items")]
        public async Task<IActionResult> AddToCart(
            [FromBody] CartRequest request)
        {
            var cart = await _cartService.AddToCartAsync(request);

            if (cart == null)
                return BadRequest();

            return Ok(cart);
        }


        [HttpPatch("items/{id}")]
        public async Task<IActionResult> UpdateQuantity(
            int id,
            [FromBody] UpdateCartQuantityRequest request)
        {
            var result = await _cartService.UpdateQuantityAsync(
                id,
                request.Quantity);

            if (!result)
                return BadRequest();

            return Ok();
        }


        [HttpDelete("items/{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var result = await _cartService.RemoveFromCartAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }


        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {
            var result = await _cartService.ClearCartAsync();

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
