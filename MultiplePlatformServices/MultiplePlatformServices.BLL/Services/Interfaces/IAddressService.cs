using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressResponse>> GetAddressesAsync(string userId);

        Task<AddressResponse> CreateAddressAsync(string userId, AddressRequest request);

        Task<AddressResponse?> UpdateAddressAsync(string userId, int addressId, AddressRequest request);

        Task<bool> DeleteAddressAsync(string userId, int addressId);

        Task<bool> SetDefaultAddressAsync(string userId, int addressId);
    }
}
