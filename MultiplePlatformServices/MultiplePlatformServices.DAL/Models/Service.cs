using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int DeliveryDays { get; set; }

        public string? Image { get; set; }

        public bool IsActive { get; set; } = true;

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public DateTime? UpdatedAt { get; set; }

        // Freelancer Foreign Key
        public string FreelancerId { get; set; } = null!;

        public ApplicationUser Freelancer { get; set; } = null!;

        // Category Foreign Key
        public int ServiceCategoryId { get; set; }

        public ServiceCategory ServiceCategory { get; set; } = null!;
    }
}
