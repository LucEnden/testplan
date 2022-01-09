using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestplanLib.Models;
using TestplanLib.Repositories;

namespace TestplanTests.Stubs
{
    internal class ItemRepositoryStub : IItemRepository
    {
        private readonly List<ItemModel> _items = new();
        public ItemRepositoryStub() { }
        public ItemRepositoryStub(List<ItemModel> items)
        {
            _items = items;
        }

        public int Create(ItemModel order)
        {
            _items.Add(order);
            return 1;
        }

        public int Delete(ItemModel itemId)
        {
            _items.Remove(itemId);
            return 1;
        }

        public ItemModel Read(string itemId)
        {
            return _items.Find(o => o.ItemId == itemId);
        }

        public ItemModel[] ReadAll()
        {
            return _items.ToArray();
        }

        public int Update(ItemModel itemId)
        {
            int index = _items.FindIndex(o => o == itemId);
            _items[index] = itemId;
            return 1;
        }
    }
}
