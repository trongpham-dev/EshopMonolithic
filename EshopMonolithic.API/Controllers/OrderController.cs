using EshopMonolithic.API.Application.Commands;
using EshopMonolithic.API.Application.Queries;
using EshopMonolithic.API.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EshopMonolithic.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderQueries _queries;

        public OrderController(IMediator mediator, ILogger<OrderController> logger, IOrderQueries queries)
        {
            _mediator = mediator;
            _logger = logger;
            _queries = queries;
        }

        [HttpPut("/cancel")]
        public async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CancelOrderAsync(CancelOrderCommand command)
        {
            _logger.LogInformation("Sending command: {CommandName} - {OrderNumber)", command.GetGenericTypeName(), command.OrderNumber);

            var commandResult = await _mediator.Send(command);

            if (!commandResult)
            {
                return TypedResults.Problem(detail: "Cancel order failed to process.", statusCode: 500);
            }

            return TypedResults.Ok();
        }
    }
}
