using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        // Cart Foreign Key
        public int CartId { get; set; }

        public Cart Cart { get; set; } = null!;

        // Product Foreign Key
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
