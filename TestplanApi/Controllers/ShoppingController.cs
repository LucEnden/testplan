using Microsoft.AspNetCore.Mvc;
using TestplanLib;
using TestplanLib.Models;
using TestplanLib.Repositories;

namespace TestplanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

        public ShoppingController(IOrderManager orderManager, IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _orderManager = orderManager;
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet("orders")]
        public OrderModel[] Orders()
        {
            return _orderRepository.ReadAll();
        }

        [HttpPost("placeorder")]
        public string PlaceOrder(string[] itemIds)
        {
            string orderId = _orderManager.PlaceOrder(itemIds);
            OrderModel model = new()
            {
                OrderId = orderId,
                Items = _itemRepository.ReadAll().Where(i => itemIds.Contains(i.ItemId)).ToList()
            };
            _orderRepository.Create(model);
            return orderId;
        }
    }
}