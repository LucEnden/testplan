using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestplanLib.Repositories;

namespace TestplanTests.Stubs
{
    internal class TestplanApiStub : WebApplicationFactory<Program>
    {
        public OrderRepositoryStub? OrderRepository { get; set; }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                if (OrderRepository != null)
                    services.AddScoped<IOrderRepository>(x => this.OrderRepository);
            });

            return base.CreateHost(builder);
        }
    }
}
