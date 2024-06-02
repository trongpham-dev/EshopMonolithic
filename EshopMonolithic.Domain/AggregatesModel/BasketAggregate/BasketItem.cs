using EshopMonolithic.Domain.SeedWork;

namespace EshopMonolithic.Domain.AggregatesModel.BasketAggregate
{
    public class BasketItem : Entity
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal OldUnitPrice { get; private  set; }
        public int Quantity { get; private set; }
        public string PictureUrl { get; private set; }

        public BasketItem(int productId, string productName, decimal unitPrice, decimal oldUnitPrice, int quantity, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            OldUnitPrice = oldUnitPrice;
            Quantity = quantity;
            PictureUrl = pictureUrl;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
