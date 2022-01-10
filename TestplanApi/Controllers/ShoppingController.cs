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

        [HttpGet("items")]
        public ItemModel[] Items()
        {
            return _itemRepository.ReadAll();
        }

        [HttpPost("item/add")]
        public ItemModel Items(string name)
        {
            ItemModel model = new()
            {
                Name = name,
                ItemId = Guid.NewGuid().ToString()
            };
            _itemRepository.Create(model);
            return model;
        }

        [HttpPost("order/add")]
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