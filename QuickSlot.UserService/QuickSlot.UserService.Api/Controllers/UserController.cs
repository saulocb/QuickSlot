using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickSlot.UserService.Application.CQRS.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickSlot.UserService.Api.Controllers  
{
    [Route("api/v1/[controller]")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        private static readonly ConcurrentDictionary<string, string> Items = new ConcurrentDictionary<string, string>();

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpGet("{key}")]
        public IActionResult Read(string key)
        {
            if (Items.TryGetValue(key, out string value))
                return Ok(new { Key = key, Value = value });
            else
                return NotFound();
        }

        [HttpPut("{key}")]
        public IActionResult Update(string key, [FromBody] string newValue)
        {
            if (Items.ContainsKey(key))
            {
                Items[key] = newValue;
                return Ok(new { Key = key, Value = newValue });
            }
            else
                return NotFound();
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            if (Items.TryRemove(key, out string value))
                return NoContent();
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            return Ok(Items);
        }
    }
}
