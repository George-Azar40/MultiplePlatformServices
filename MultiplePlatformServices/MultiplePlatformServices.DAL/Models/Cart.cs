using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // The user who owns the cart
        public string UserId { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
