
using EshopMonolithic.Domain.SeedWork;

namespace EshopMonolithic.Domain.AggregatesModel.BasketAggregate
{
    public class Basket : Entity, IAggregateRoot
    {
        public string BuyerId { get; private set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

        public Basket() { }

        public Basket(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(int productId, string productName, decimal unitPrice, decimal oldUnitPrice, int quantity, string pictureUrl)
        {
            if (!Items.Any(i => i.ProductId == productId))
            {
                _items.Add(new BasketItem(productId, productName, unitPrice, oldUnitPrice, quantity, pictureUrl));
                return;
            }
            var existingItem = Items.First(i => i.ProductId == productId);
            existingItem.AddQuantity(quantity);
        }

        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }

        public void SetNewBuyerId(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}
