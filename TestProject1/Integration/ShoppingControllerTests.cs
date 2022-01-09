using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestplanTests.Stubs;
using System.Collections.Generic;
using TestplanLib.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Newtonsoft.Json;
using TestplanLib;

namespace TestplanTests.Integration
{
    public class ShoppingControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact] 
        public async Task GET_retrieves_orders() 
        {
            // Arange

            // Create orders to test
            List<ItemModel> items = new()
            {
                new ItemModel() { ItemId = "00000000-0000-0000-0000-000000000000" }
            };
            List<OrderModel> orders = new()
            {
                new() { OrderId = "00000000-0000-0000-0000-000000000000", Items = items }
            };
            // Instantiate API stub
            TestplanApiStub stubApi = new TestplanApiStub();
            // Set order repository (required as its used within the endpoint body method
            stubApi.OrderRepository = new OrderRepositoryStub(orders);
            HttpClient client = stubApi.CreateClient();

            // Act

            // Excecute request and convert response content to order model list
            var response = await client.GetAsync("/api/Shopping/orders");
            var responseBody = await response.Content.ReadFromJsonAsync<List<OrderModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, responseBody.Count);
        }


        [Fact]
        public async Task POST_orders_get_placed()
        {
            // Arange
            string[] itemsToOrder =
            {
                "00000000-0000-0000-0000-000000000000",
                "00000000-0000-0000-0000-000000000001"
            };
            TestplanApiStub stubApi = new TestplanApiStub();
            stubApi.OrderRepository = new OrderRepositoryStub();
            stubApi.ItemRepository = new ItemRepositoryStub();
            stubApi.OrderManager = new OrderManager();
            HttpClient client = stubApi.CreateClient();

            var httpContent = new StringContent(JsonConvert.SerializeObject(itemsToOrder), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Shopping/placeorder", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
