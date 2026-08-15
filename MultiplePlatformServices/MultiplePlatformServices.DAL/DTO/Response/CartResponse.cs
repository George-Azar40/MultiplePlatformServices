namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductImage { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class CartResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
        public decimal GrandTotal => Items.Sum(i => i.TotalPrice);
    }
}
