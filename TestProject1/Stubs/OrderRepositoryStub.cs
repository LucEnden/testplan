using System.Collections.Generic;
using TestplanLib.Models;
using TestplanLib.Repositories;

namespace TestplanTests.Stubs
{
    public class OrderRepositoryStub : IOrderRepository
    {
        private readonly List<OrderModel> _orders = new();
        public OrderRepositoryStub() { }
        public OrderRepositoryStub(List<OrderModel> orders)
        {
            _orders = orders;
        }

        public int Create(OrderModel order)
        {
            _orders.Add(order);
            return 1;
        }

        public int Delete(OrderModel orderId)
        {
            _orders.Remove(orderId);
            return 1;
        }

        public OrderModel Read(string orderId)
        {
            return _orders.Find(o => o.OrderId == orderId);
        }

        public OrderModel[] ReadAll()
        {
            return _orders.ToArray();
        }

        public int Update(OrderModel orderId)
        {
            int index = _orders.FindIndex(o => o == orderId);
            _orders[index] = orderId;
            return 1;
        }
    }
}
