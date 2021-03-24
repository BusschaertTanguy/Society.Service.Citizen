using System;
using System.Threading.Tasks;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CitizensController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitizensController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitizen([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 25)
        {
            var query = new GetCitizens.Query(pageIndex, pageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCitizen(Guid id)
        {
            var query = new GetCitizen.Query(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}