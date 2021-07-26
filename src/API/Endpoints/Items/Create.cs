using Application.Interfaces;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
            Summary = "Create a set of items",
            Description = "Create a set of items",
            OperationId = "Item.Create",
            Tags = new[] { "ItemEndpoint" })
        ]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateItemCommand request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var item = RepositoryMapper.MapItem(request);

            _repository.Add(item);

            return Created("items", item);
        }
    }
}
