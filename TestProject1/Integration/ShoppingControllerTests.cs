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
using System;

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
        public async Task POST_order_get_created()
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
            string guidPattern = @"([\d\w]{8})-([\d\w]{4})-([\d\w]{4})-([\d\w]{4})-([\d\w]{12})";

            string content = JsonConvert.SerializeObject(itemsToOrder);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Shopping/orders/add", httpContent);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Matches(guidPattern, responseBody);
        }

        [Fact]
        public async Task GET_retrieves_items()
        {
            // Arange

            // Create orders to test
            List<ItemModel> items = new()
            {
                new ItemModel() { ItemId = "00000000-0000-0000-0000-000000000000" }
            };
            // Instantiate API stub
            TestplanApiStub stubApi = new TestplanApiStub();
            // Set order repository (required as its used within the endpoint body method
            stubApi.ItemRepository = new ItemRepositoryStub(items);
            HttpClient client = stubApi.CreateClient();

            // Act

            // Excecute request and convert response content to order model list
            var response = await client.GetAsync("/api/Shopping/items");
            var responseBody = await response.Content.ReadFromJsonAsync<List<ItemModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, responseBody.Count);
        }

        [Fact]
        public async Task POST_items_get_created()
        {
            // Arange
            string name = "testItem";
            TestplanApiStub stubApi = new TestplanApiStub();
            stubApi.ItemRepository = new ItemRepositoryStub();
            HttpClient client = stubApi.CreateClient();
            string guidPattern = @"([\d\w]{8})-([\d\w]{4})-([\d\w]{4})-([\d\w]{4})-([\d\w]{12})";

            string content = JsonConvert.SerializeObject(name);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Shopping/items/add", httpContent);
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(stubApi.ItemRepository.Items);
            Assert.Matches(guidPattern, responseBody);
        }

        [Fact]
        public async Task PUT_items_name_gets_updated()
        {
            // Arange
            List<ItemModel> items = new()
            {
                new() { ItemId = "00000000-0000-0000-0000-000000000000" , Name = "name" }
            };
            TestplanApiStub stubApi = new TestplanApiStub();
            stubApi.ItemRepository = new ItemRepositoryStub(items);
            HttpClient client = stubApi.CreateClient();

            // Act
            items[0].Name = "updatedName";
            string content = JsonConvert.SerializeObject(items[0]);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/Shopping/items/update", httpContent);
            /*var responseBody = await response.Content.ReadAsStringAsync();*/

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(stubApi.ItemRepository.Items[0].Name, items[0].Name);
        }
    }
}