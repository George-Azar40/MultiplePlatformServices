using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public int StoreId { get; set; }
        public string? StoreName { get; set; }
        public int ProductCategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
