using API.Endpoints.Items;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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

        public override async Task<ActionResult> HandleAsync(CreateOrderCommand request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var order = RepositoryMapper.MapOrder(request);

            var orderCalculator = new OrderCalculator();

            order.Total = orderCalculator.CalculateTotal(order); 

            return Created("order", order);
        }
    }
}
