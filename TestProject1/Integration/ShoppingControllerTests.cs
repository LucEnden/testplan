using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestplanTests.Stubs;
using System.Collections.Generic;
using TestplanLib.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestplanTests.Integration
{
    public class ShoppingControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        [Fact] 
        public async Task GET_retrieves_orders() 
        {
            // Arange
            List<ItemModel> items = new()
            {
                new ItemModel() { ItemId = "00000000-0000-0000-0000-000000000000" }
            };
            List<OrderModel> orders = new()
            {
                new() { OrderId = "00000000-0000-0000-0000-000000000000", Items = items }
            };
            TestplanApiStub stubApi = new TestplanApiStub();
            stubApi.OrderRepository = new OrderRepositoryStub(orders);
            HttpClient client = stubApi.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Shopping/orders");
            var responseBody = await response.Content.ReadFromJsonAsync<List<OrderModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, responseBody.Count);
        }
    }
}
