namespace WebApi.Dtos
{
    public class SaleCreateDto
    {
        public List<SaleItemDto> Items { get; set; } = new();
    }
}
