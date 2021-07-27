using API;
using API.Endpoints.Orders;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
                OrderItems = Enumerable.Repeat(new OrderItem() { Id = "A", Amount = 30 }, 1)
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/order", stringContent);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();


            var order = JsonConvert.DeserializeObject<Order>(stringResponse);

            order.Total.Should().Be(30);
        }
    }
}
