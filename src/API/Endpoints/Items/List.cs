﻿using Application.Interfaces;
using Infrastructure.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Endpoints.Items
{
    public class List : BaseAsyncEndpoint.WithoutRequest.WithResponse<ListItemResult>
    {
        private readonly IRepository _repository;

        public List(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("items")]
        [SwaggerOperation(
            Summary = "List all items",
            Description = "List all items",
            OperationId = "Item.List",
            Tags = new[] { "ItemEndpoint" })
        ]
        [Consumes(MediaTypeNames.Application.Json)] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public override async Task<ActionResult<ListItemResult>> HandleAsync()
        {
            var items = _repository.Get().ToArray();

            if (!items.Any())
            {
                return NoContent();
            }

            return Ok(new ListItemResult() { Items = items });
        }
    }
}