using MultiplePlatformServices.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class StoreResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Logo { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;


        // Foreign Key
        public string VendorId { get; set; } = null!;

        // Navigation Property
        public ApplicationUser Vendor { get; set; } = null!;

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
