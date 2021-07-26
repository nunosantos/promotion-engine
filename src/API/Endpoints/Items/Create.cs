using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateItemCommand>.WithoutResponse
    {
        [HttpPost("items")]
        [SwaggerOperation(
            Summary = "Create a set of items",
            Description = "Create a set of items",
            OperationId = "Item.Create",
            Tags = new[] { "ItemEndpoint" })
        ]
        public override Task<ActionResult> HandleAsync([FromBody] CreateItemCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
