using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        // Order Foreign Key
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;

        // Product Foreign Key
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
