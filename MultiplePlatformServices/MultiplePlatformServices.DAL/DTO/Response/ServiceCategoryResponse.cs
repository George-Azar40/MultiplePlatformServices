namespace MultiplePlatformServices.DAL.DTO.Response
{
    public class ServiceCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
