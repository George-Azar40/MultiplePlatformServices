using MultiplePlatformServices.DAL.Models;

namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class ServiceOrderResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? DeliveryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string CustomerId { get; set; } = null!;
        public string? CustomerName { get; set; }
        public string FreelancerId { get; set; } = null!;
        public string? FreelancerName { get; set; }
        public int ServiceId { get; set; }
        public string? ServiceTitle { get; set; }
    }
}
