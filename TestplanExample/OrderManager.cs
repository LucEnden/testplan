using TestplanLib.Models;

namespace TestplanLib
{
    public class OrderManager : IOrderManager
    {
        public List<ItemModel> Items { get; set; } = new()
        {
            new() { ItemId = "00000000-0000-0000-0000-000000000000" },
            new() { ItemId = "00000000-0000-0000-0000-000000000001" }
        };

        public string PlaceOrder(string[] itemIDs)
        {
            if (itemIDs.Length == 0)
                throw new ArgumentException("At least one item ID needs to be specified.");

            if (itemIDs.Any(i => string.IsNullOrEmpty(i) || string.IsNullOrWhiteSpace(i)))
                throw new ArgumentNullException("Item ID can not be null nor empty.");

            List<ItemModel> itemsToOrder = new();
            foreach (string i in itemIDs)
            {
                ItemModel? item = Items.Find(x => x.ItemId == i);
                if (item == null)
                {
                    throw new KeyNotFoundException("Item does not exist.");
                }
                else
                {
                    itemsToOrder.Add(item);
                }
            }

            // place order logic

            return Guid.NewGuid().ToString();
        }
    }
}