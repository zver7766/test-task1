using System.Threading.Tasks;
using API.Commands;
using Core.Entities;
using Core.Intefraces;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClickController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ClickController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Increases the number of click on the selected ad by +1
        /// </summary>
        /// <returns>An int which represents a success or neither status of the Db</returns>
        [HttpPost("AdClick")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AdClick(int id)
        {
            var command = new AdClickCommand(id);
            var result = await _mediator.Send(command);
            return result == 0 ? NotFound("Ad was not found / Ad with this id inactive") : Ok(result);
        }
    }
}
