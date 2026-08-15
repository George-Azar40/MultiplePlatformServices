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
        public bool IsActive { get; set; }
        public string VendorId { get; set; } = null!;
        public string? VendorName { get; set; }
    }
}
