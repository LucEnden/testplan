using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestplanLib.Repositories;
using TestplanLib;

namespace TestplanTests.Stubs
{
    internal class TestplanApiStub : WebApplicationFactory<Program>
    {
        public OrderRepositoryStub? OrderRepository { get; set; }
        public ItemRepositoryStub? ItemRepository { get; set; }
        public OrderManager? OrderManager { get; set; }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                if (OrderRepository != null)
                    services.AddScoped<IOrderRepository>(x => OrderRepository);
                if (ItemRepository != null)
                    services.AddScoped<IItemRepository>(x => ItemRepository);
                if (OrderManager != null)
                    services.AddScoped<IOrderManager>(x => OrderManager);
            });

            return base.CreateHost(builder);
        }
    }
}
