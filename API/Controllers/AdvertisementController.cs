using System.Collections.Generic;
using System.Threading.Tasks;
using API.Commands;
using API.Dtos;
using API.Queries;
using Core.Entities;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdvertisementController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AdvertisementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates and map an advertisement in a Db, using Advertisement Service
        /// </summary>
        /// <returns>Created advertisement</returns>
        [HttpPost("CreateAd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdvertisementToReturnDto>> CreateAd([FromQuery] AdvertisementToCreate adCreateParams)
        {
            var command = new CreateAdCommand(adCreateParams);
            var result = await _mediator.Send(command);
            return result != null ? Ok(result) : BadRequest("Add was not created");
        }

        /// <summary>
        ///    Have a simple queue of ads which id`s stores in a cache
        /// </summary>
        /// <returns>Mapped advertisement</returns>
        [HttpGet("GetAd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AdvertisementToReturnDto>> GetAd()
        {
            var query = new GetAdQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        ///  Retrieves a criteria and then sort by using them
        /// </summary>
        /// <returns>Mapped advertisement</returns>
        [HttpGet("GetAdByCriteria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<AdvertisementToReturnDto>>> GetAdByCriteria([FromQuery] AdvertisementSpecParams adParams)
        {
            var query = new GetAdByCriteriaQuery(adParams);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Ad was not found / Invalid criteria");
        }

        /// <summary>
        /// Delete or deactivate an advertisement
        /// </summary>
        /// <returns>An int which represents a success or neither status of the Db</returns>
        [HttpDelete("DeleteAd")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> DeleteAd(int id, bool deactivate = true)
        {
            var command = new DeleteAdCommand(id,deactivate);
            var result = await _mediator.Send(command);
            return result != 0 ? Ok(result) : NotFound("Ad was not found / Delete was unsuccessful");
        }

        /// <summary>
        /// Method for gaining statistics 
        /// </summary>
        /// <returns>An array of ordered ads by any criteria</returns>
        [HttpGet("Stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<StatisticToReturnDto>>> Statistics([FromQuery] AdvertisementOrderBySpecParams adOrderByParams)
        {
            var query = new StatsQuery(adOrderByParams);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Stat rejected");
        }

    }
}
