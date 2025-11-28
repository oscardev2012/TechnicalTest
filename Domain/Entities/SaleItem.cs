using Domain.Commons;

namespace Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public int ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int SaleId { get; set; }
        public decimal Total => UnitPrice * Quantity;

        public SaleItem(int productId, decimal unitPrice, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
