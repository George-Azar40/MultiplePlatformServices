using MultiplePlatformServices.DAL.DTO.Request;
using MultiplePlatformServices.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse?> GetCartAsync();

        Task<CartResponse?> AddToCartAsync(CartRequest request);

        Task<bool> UpdateQuantityAsync(int id, int quantity);

        Task<bool> RemoveFromCartAsync(int id);

        Task<bool> ClearCartAsync();
    }
}
