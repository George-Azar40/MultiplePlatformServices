using MultiplePlatformServices.DAL.Models;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public class ServiceOrderRequset
    {
        public int ServiceId { get; set; }
        public string? Description { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
