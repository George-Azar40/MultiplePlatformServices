using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _services;
        public ServiceController(IServiceService services)
        {
            _services   = services;     
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var services = await _services.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var service = await _services.GetByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpGet("title/{title}")]

        public async Task<IActionResult> GetByName(string title)
        {
            var service = await _services.GetByTitle(title);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(ServiceRequset requset)
        {
            var result = await _services.CreateAsync(requset);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ServiceRequset requset)
        {
            var isUpdated = await _services.UpdateAsync(id, requset);
            if (!isUpdated) return BadRequest(new
            {
                message = false
            });
            return Ok(new
            {
                message = true,
                isUpdated
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _services.DeleteAsync(id);
            if (!isDeleted) return BadRequest(new
            {
                message = false 
            });
            return Ok(new
            {
                message = true,
                isDeleted
            });
        }
    }
}
