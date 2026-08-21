using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<List<ServiceCategoryResponse?>> GetAllAsync();
        Task<ServiceCategoryResponse?> GetById(int id);
        Task<ServiceCategoryResponse?> GetByName(string name);
        Task<ServiceCategoryResponse> CreateAsync(ServiceCategoryRequest request);
        Task<bool> UpdateAsync(int id, ServiceCategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
