using API.Endpoints.Items;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Integration_tests.ItemEndpoint
{

    public class Create
    {
        private readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task CreateItems()
        {
            var createOrderItemsCommand = new CreateItemCommand();

            var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/items", stringContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
