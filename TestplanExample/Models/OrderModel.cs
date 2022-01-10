namespace TestplanLib.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
