using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using System.Security.Claims;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Get all addresses for the authenticated user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var addresses = await _addressService.GetAddressesAsync(userId);
            return Ok(addresses);
        }

        /// <summary>
        /// Create a new address for the authenticated user.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] AddressRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var created = await _addressService.CreateAddressAsync(userId, request);
            return CreatedAtAction(nameof(GetAddresses), created);
        }

        /// <summary>
        /// Update an address. Only the owner can update their own address.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var updated = await _addressService.UpdateAddressAsync(userId, id, request);
            if (updated == null)
                return NotFound(new { message = "Address not found or you do not own it." });

            return Ok(updated);
        }

        /// <summary>
        /// Delete an address. Only the owner can delete their own address.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var deleted = await _addressService.DeleteAddressAsync(userId, id);
            if (!deleted)
                return NotFound(new { message = "Address not found or you do not own it." });

            return Ok(new { message = "Address deleted successfully." });
        }

        /// <summary>
        /// Set an address as the default shipping address.
        /// </summary>
        [HttpPost("{id:int}/set-default")]
        public async Task<IActionResult> SetDefault(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var success = await _addressService.SetDefaultAddressAsync(userId, id);
            if (!success)
                return NotFound(new { message = "Address not found or you do not own it." });

            return Ok(new { message = "Default address updated successfully." });
        }
    }
}
