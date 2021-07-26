using API;
using API.Endpoints.Items;
using Domain.Orders;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Integration_tests.ItemEndpoint
{
    public class List : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public List(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task ListAllItems()
        {
            var createOrderItemsCommand = new CreateItemCommand()
            {
                Items = new List<Item>
                {
                    new () { Id = 'A', UnitPrice = 50},
                    new () { Id = 'B', UnitPrice = 30},
                    new () { Id = 'C', UnitPrice = 20},
                    new () { Id = 'D', UnitPrice = 15},
                }
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/items", stringContent);

            response.EnsureSuccessStatusCode();

            var itemsResponse = await client.GetAsync("/items");

            var stringResponse = await itemsResponse.Content.ReadAsStringAsync();

            var itemResult = JsonSerializer.Deserialize<ListIItemResult>(stringResponse);

            itemResult.Items.Should().HaveCount(4);
            itemResult.Items.ToList()[3].Id.Should().Be('D');
            itemResult.Items.ToList()[3].UnitPrice.Should().Be(15);
        }

    }
}
