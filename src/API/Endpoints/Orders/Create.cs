using API.Endpoints.Items;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Orders
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreateOrderCommand>.WithoutResponse
    {
        private readonly IRepository _repository;

        public Create(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("order")]
        [SwaggerOperation(
            Summary = "Creates an order",
            Description = "Creates an order",
            OperationId = "Order.Create",
            Tags = new[] { "CreateEndpoint" })
        ]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult> HandleAsync(CreateOrderCommand request)
        {
            if (request is null)
            {
                BadRequest();
            }

            var existingItems = _repository.Get();


            var order = RepositoryMapper.MapOrder(request, existingItems);

            var orderCalculator = new OrderCalculator(_repository);

            order.Total = orderCalculator.CalculateItemTotal(order);

            return Created("order", order);
        }
    }
}