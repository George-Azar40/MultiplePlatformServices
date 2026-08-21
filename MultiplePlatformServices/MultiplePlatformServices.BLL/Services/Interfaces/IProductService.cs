using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse?>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse> CreateAsync(ProductRequest request);
        Task<bool> UpdateAsync(int id, ProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
