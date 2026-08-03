using MultiplePlatformServices.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Repository.RepositoryInterfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>?> DecreaseQuantityAsync(List<OrderItem> orderItems);
    }
}
