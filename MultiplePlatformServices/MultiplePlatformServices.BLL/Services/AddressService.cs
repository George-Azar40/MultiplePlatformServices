using Mapster;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<AddressResponse>> GetAddressesAsync(string userId)
        {
            var addresses = await _addressRepository.GetAllAsync(a => a.UserId == userId);
            return addresses.Adapt<List<AddressResponse>>();
        }

        public async Task<AddressResponse> CreateAddressAsync(string userId, AddressRequest request)
        {
            if (request.IsDefault)
            {
                var existingDefaults = await _addressRepository.GetAllAsync(a => a.UserId == userId && a.IsDefault);
                if (existingDefaults.Any())
                {
                    foreach (var ex in existingDefaults)
                    {
                        ex.IsDefault = false;
                    }
                    await _addressRepository.UpdateRangeAsync(existingDefaults);
                }
            }

            var userAddresses = await _addressRepository.GetAllAsync(a => a.UserId == userId);
            bool isFirstAddress = !userAddresses.Any();

            var newAddress = request.Adapt<Address>();
            newAddress.UserId = userId;
            if (isFirstAddress)
            {
                newAddress.IsDefault = true;
            }

            var created = await _addressRepository.CreateAsync(newAddress);
            return created.Adapt<AddressResponse>();
        }

        public async Task<AddressResponse?> UpdateAddressAsync(string userId, int addressId, AddressRequest request)
        {
            var address = await _addressRepository.GetOne(a => a.Id == addressId && a.UserId == userId);
            if (address == null) return null;

            if (request.IsDefault && !address.IsDefault)
            {
                var existingDefaults = await _addressRepository.GetAllAsync(a => a.UserId == userId && a.IsDefault && a.Id != addressId);
                if (existingDefaults.Any())
                {
                    foreach (var ex in existingDefaults)
                    {
                        ex.IsDefault = false;
                    }
                    await _addressRepository.UpdateRangeAsync(existingDefaults);
                }
            }

            request.Adapt(address);
            address.UserId = userId; // security lock

            var allAddresses = await _addressRepository.GetAllAsync(a => a.UserId == userId);
            if (allAddresses.Count == 1)
            {
                address.IsDefault = true;
            }

            await _addressRepository.UpdateAsync(address);
            return address.Adapt<AddressResponse>();
        }

        public async Task<bool> DeleteAddressAsync(string userId, int addressId)
        {
            var address = await _addressRepository.GetOne(a => a.Id == addressId && a.UserId == userId);
            if (address == null) return false;

            bool wasDefault = address.IsDefault;
            await _addressRepository.DeleteAsync(address);

            if (wasDefault)
            {
                var remaining = await _addressRepository.GetAllAsync(a => a.UserId == userId);
                var newDefault = remaining.FirstOrDefault();
                if (newDefault != null)
                {
                    newDefault.IsDefault = true;
                    await _addressRepository.UpdateAsync(newDefault);
                }
            }

            return true;
        }

        public async Task<bool> SetDefaultAddressAsync(string userId, int addressId)
        {
            var target = await _addressRepository.GetOne(a => a.Id == addressId && a.UserId == userId);
            if (target == null) return false;

            if (target.IsDefault) return true;

            var existingDefaults = await _addressRepository.GetAllAsync(a => a.UserId == userId && a.IsDefault);
            foreach (var ex in existingDefaults)
            {
                ex.IsDefault = false;
            }
            if (existingDefaults.Any())
            {
                await _addressRepository.UpdateRangeAsync(existingDefaults);
            }

            target.IsDefault = true;
            await _addressRepository.UpdateAsync(target);
            return true;
        }
    }
}
