using System;
using System.Threading.Tasks;
using Application.Commands;
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

        [HttpPost]
        public async Task<IActionResult> CreateCitizen([FromBody] CreateCitizen.Command command)
        {
            await _mediator.Send(command);
            return Created("api/citizens", command.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateCitizen(Guid id)
        {
            var request = new GetCitizen.Request(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}