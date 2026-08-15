using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DeliveryDays { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
        public string FreelancerId { get; set; } = null!;
        public string? FreelancerName { get; set; }
        public int ServiceCategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
