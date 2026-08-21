using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            if(products is null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            var createdProduct = await _productService.CreateAsync(request);
            return Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductRequest request)
        {
            var updatedProduct = await _productService.UpdateAsync(id, request);
            if (!updatedProduct) return BadRequest();
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.DeleteAsync(id);
            if (!deletedProduct) return BadRequest();
            return Ok(deletedProduct);
        }
    }
}
