using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _productCategoryService.GetAllAsync();
            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _productCategoryService.GetByIdAsync(id);
            if(category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ProductCategoryRequest request)
        {
            var result = await _productCategoryService.CreateAsync(request);
            if(result == null) return NotFound();
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,[FromBody] ProductCategoryRequest request)
        {
            var result = await _productCategoryService.UpdateAsync(id,request);
            if(result == null) return NotFound();
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productCategoryService.DeleteAsync(id);
            if(result == null) return NotFound();
            return Ok(result);
        }


        
    }
}
