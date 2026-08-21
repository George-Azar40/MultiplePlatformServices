using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceResponse>> GetAllAsync();
        Task<ServiceResponse?> GetByIdAsync(int id);
        Task<ServiceResponse?> GetByTitle(string title);
        Task<ServiceResponse> CreateAsync(ServiceRequset request);
        Task<bool> UpdateAsync(int id, ServiceRequset requset);
        Task<bool> DeleteAsync(int id);

    }
}
