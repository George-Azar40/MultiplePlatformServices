using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;

namespace MultiplePlatformServices.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var stores =  await _storeService.GetAllStores();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var store = await _storeService.GetStoreById(id);
            if(store == null) return BadRequest();
            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreRequest request)
        {
            var result = await _storeService.CreateStore(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StoreRequest request)
        {
            var result = await _storeService.UpdateStore(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _storeService.DeleteStore(id);
            if(!result) return NotFound();
            return Ok(result);
        }
    }
}
