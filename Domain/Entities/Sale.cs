using Domain.Commons;

namespace Domain.Entities
{
    public class Sale
    {
        public int Id { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal Total { get; private set; }

        public List<SaleItem> Items { get; private set; } = new();

        // EF requires this
        private Sale() { }

        // Domain constructor
        public Sale(IEnumerable<SaleItem> items)
        {
            Items = items.ToList();
            SaleDate = DateTime.UtcNow;
        }
    }

}
