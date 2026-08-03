using MultiplePlatformServices.DAL.Data;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Repository
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Product>?> DecreaseQuantityAsync(List<OrderItem> orderItems)
        {
            var productIds = orderItems.Select(p=>p.ProductId).ToList();
            var products = await GetAllAsync(p => productIds.Contains(p.Id));

            foreach(var product in products)
            {
                var item = orderItems.FirstOrDefault(p=>p.ProductId == product.Id);
                product.StockQuantity -= item.Quantity;
            }


            await UpdateRangeAsync(products);

            return products.Where(p => p.StockQuantity < 5).ToList();

        }
    }
}
