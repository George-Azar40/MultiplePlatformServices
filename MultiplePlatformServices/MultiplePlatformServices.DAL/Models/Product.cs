using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Image { get; set; }

        public bool IsActive { get; set; } = true;

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public DateTime? UpdatedAt { get; set; }

        // Store Foreign Key
        public int StoreId { get; set; }

        public Store Store { get; set; } = null!;

        // Category Foreign Key
        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; } = null!;
    }
}
