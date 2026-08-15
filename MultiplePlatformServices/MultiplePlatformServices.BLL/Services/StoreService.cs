using Mapster;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storerepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storerepository = storeRepository;
        }

        public async Task<List<StoreResponse>> GetAllStores()
        {
            var stores = await _storerepository.GetAllAsync();
            var result = stores.Adapt<List<StoreResponse>>();
            return result;
        }


        public async Task<StoreResponse?> GetStoreById(int id)
        {
            var store = await _storerepository.GetOne(s => s.Id == id, []);
            var result = store.Adapt<StoreResponse>();
            return result;
        }

        public async Task<StoreResponse> CreateStore(StoreRequest store)
        {
            var newStore = store.Adapt<Store>();
            await _storerepository.CreateAsync(newStore);
            return newStore.Adapt<StoreResponse>();
        }

        public async Task<bool> DeleteStore(int id)
        {
            throw new NotImplementedException();
        }
        

        public async Task<StoreResponse> UpdateStore(int id, StoreRequest store)
        {
            throw new NotImplementedException();
        }
    }
}
