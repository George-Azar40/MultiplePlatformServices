using MultiplePlatformServices.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public class ServiceRequset
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DeliveryDays { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; } = true;
        public int ServiceCategoryId { get; set; }
        public string FreelancerId { get; set; }
    }
}
