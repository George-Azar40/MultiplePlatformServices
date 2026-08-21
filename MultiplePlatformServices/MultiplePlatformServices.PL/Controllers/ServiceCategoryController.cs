using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryService _serviceCategoryService;
        public ServiceCategoryController(IServiceCategoryService serviceCategoryService)
        {
            _serviceCategoryService = serviceCategoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _serviceCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _serviceCategoryService.GetById(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _serviceCategoryService.GetByName(name);
            if (category == null) return NotFound();
            return Ok(category);
        }


        [HttpPost("")]
        public async Task<IActionResult> Create(ServiceCategoryRequest request)
        {
            var result = await _serviceCategoryService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServiceCategoryRequest request)
        {
            var result = await _serviceCategoryService.UpdateAsync(id, request);
            return Ok(new
                {
                    message = "Updated",
                    request
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceCategoryService.DeleteAsync(id);
            return Ok(new
            {
                message = "Deleted",
                result
            });
        }

    }
}
