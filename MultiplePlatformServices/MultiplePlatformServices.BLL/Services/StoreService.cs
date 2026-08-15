using Mapster;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<List<StoreResponse>> GetAllStores()
        {
            var stores = await _storeRepository.GetAllAsync(includes: new[] { "Vendor" });
            return stores.Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Logo = s.Logo,
                Address = s.Address,
                Phone = s.Phone,
                IsActive = s.IsActive,
                VendorId = s.VendorId,
                VendorName = s.Vendor?.FullName
            }).ToList();
        }

        public async Task<StoreResponse?> GetStoreById(int id)
        {
            var store = await _storeRepository.GetOne(s => s.Id == id, new[] { "Vendor" });
            if (store == null) return null;
            return new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Logo = store.Logo,
                Address = store.Address,
                Phone = store.Phone,
                IsActive = store.IsActive,
                VendorId = store.VendorId,
                VendorName = store.Vendor?.FullName
            };
        }

        public async Task<StoreResponse> CreateStore(string vendorId, StoreRequest request)
        {
            var newStore = new Store
            {
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,
                Address = request.Address,
                Phone = request.Phone,
                IsActive = request.IsActive,
                VendorId = vendorId
            };
            await _storeRepository.CreateAsync(newStore);
            return new StoreResponse
            {
                Id = newStore.Id,
                Name = newStore.Name,
                Description = newStore.Description,
                Logo = newStore.Logo,
                Address = newStore.Address,
                Phone = newStore.Phone,
                IsActive = newStore.IsActive,
                VendorId = newStore.VendorId
            };
        }

        public async Task<StoreResponse?> UpdateStore(string vendorId, int id, StoreRequest request)
        {
            var store = await _storeRepository.GetOne(s => s.Id == id && s.VendorId == vendorId);
            if (store == null) return null;

            store.Name = request.Name;
            store.Description = request.Description;
            store.Logo = request.Logo;
            store.Address = request.Address;
            store.Phone = request.Phone;
            store.IsActive = request.IsActive;

            await _storeRepository.UpdateAsync(store);
            return new StoreResponse
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                Logo = store.Logo,
                Address = store.Address,
                Phone = store.Phone,
                IsActive = store.IsActive,
                VendorId = store.VendorId
            };
        }

        public async Task<bool> DeleteStore(string vendorId, int id)
        {
            var store = await _storeRepository.GetOne(s => s.Id == id && s.VendorId == vendorId);
            if (store == null) return false;
            return await _storeRepository.DeleteAsync(store);
        }

        // Admin / public overloads (no ownership check)
        public async Task<StoreResponse> CreateStore(StoreRequest store) => await CreateStore(store.VendorId, store);
        public async Task<StoreResponse> UpdateStore(int id, StoreRequest store) => (await UpdateStore(store.VendorId, id, store))!;
        public async Task<bool> DeleteStore(int id)
        {
            var s = await _storeRepository.GetOne(x => x.Id == id);
            if (s == null) return false;
            return await _storeRepository.DeleteAsync(s);
        }
    }
}
