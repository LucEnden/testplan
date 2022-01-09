using TestplanLib.Models;

namespace TestplanLib.Repositories
{
    /// <summary>
    /// Repository for order CRUDs
    /// </summary>
    public interface IOrderRepository
    {
        public int Create(OrderModel order);
        public OrderModel Read(string orderId);
        public OrderModel[] ReadAll ();
        public int Update(OrderModel orderId);
        public int Delete(OrderModel orderId);
    }
}
