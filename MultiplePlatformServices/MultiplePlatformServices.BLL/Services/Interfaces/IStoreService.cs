using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IStoreService
    {
        Task<List<StoreResponse>> GetAllStores();
        Task<StoreResponse?> GetStoreById(int id);
        Task<StoreResponse> CreateStore(StoreRequest store);
        Task<StoreResponse> UpdateStore(int id , StoreRequest store);
        Task<bool> DeleteStore(int id);
    }
}
