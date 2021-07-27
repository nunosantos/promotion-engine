using Application.Interfaces;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateItemCommand>.WithoutResponse
    {
        private readonly IRepository _repository;

        public Create(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("items")]
        [SwaggerOperation(
            Summary = "CreateOrderCommand a set of items",
            Description = "CreateOrderCommand a set of items",
            OperationId = "Product.CreateOrderCommand",
            Tags = new[] { "ItemEndpoint" })
        ]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateItemCommand request)
        {
            if (request is null)
            {
                BadRequest();
            }


            var item = RepositoryMapper.MapItem(request, _repository);

            _repository.Add(item);

            return Created("items", item);
        }
    }
}