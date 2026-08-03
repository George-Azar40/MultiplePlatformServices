using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.Models
{
    public enum ServiceOrderStatus
    {
        Pending = 1,
        Accepted = 2,
        InProgress= 3,
        Delivered =4,
        Completed = 5,
        Cancelled = 6
    }
    public class ServiceOrder
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public ServiceOrderStatus Status { get; set; } = ServiceOrderStatus.Pending;

        public DateTime? DeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        // Customer Foreign Key
        public string CustomerId { get; set; } = null!;

        public ApplicationUser Customer { get; set; } = null!;

        // Freelancer Foreign Key
        public string FreelancerId { get; set; } = null!;

        public ApplicationUser Freelancer { get; set; } = null!;

        // Service Foreign Key
        public int ServiceId { get; set; }

        public Service Service { get; set; } = null!;
    }
}
