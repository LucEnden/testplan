using TestplanLib.Models;

namespace TestplanLib.Repositories
{
    public interface IItemRepository
    {
        public int Create(ItemModel item);
        public ItemModel Read(string itemId);
        public ItemModel[] ReadAll();
        public int Update(ItemModel itemId);
        public int Delete(ItemModel itemId);
    }
}
