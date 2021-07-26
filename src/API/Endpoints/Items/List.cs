using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class List : BaseAsyncEndpoint.WithoutRequest.WithResponse<ListIItemResult>
    {
        public override Task<ActionResult<ListIItemResult>> HandleAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
