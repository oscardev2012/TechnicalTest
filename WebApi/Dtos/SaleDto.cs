namespace WebApi.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public int ItemsCount { get; set; }
    }
}
