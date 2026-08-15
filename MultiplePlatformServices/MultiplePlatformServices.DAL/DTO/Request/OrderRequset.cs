using MultiplePlatformServices.DAL.Models;

namespace MultiplePlatformServices.DAL.DTO.Request
{
    public class OrderRequset
    {
        public string ShippingAddress { get; set; } = null!;
        // Items are pulled from the user's cart at checkout
    }
}
