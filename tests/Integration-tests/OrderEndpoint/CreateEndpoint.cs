using API;
using API.Endpoints.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Integration_tests.OrderEndpoint
{
    public class CreateEndpoint : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public CreateEndpoint(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder()
        {
            var createOrderItemsCommand = new CreateOrderCommand()
            {
                OrderItems = Enumerable.Repeat(new OrderItem() { Id = "A", Amount = 30 }, 2)
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/items", stringContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();

            var total = JsonSerializer.Deserialize<int>(stringResponse);

            total.Should().Be(30);
        }
    }
}
