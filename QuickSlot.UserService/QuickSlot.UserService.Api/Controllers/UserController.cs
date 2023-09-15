using Microsoft.AspNetCore.Mvc;
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
        private static readonly ConcurrentDictionary<string, string> Items = new ConcurrentDictionary<string, string>();

        [HttpPost]
        public IActionResult Create([FromBody] KeyValuePair<string, string> item)
        { 
            if (Items.TryAdd(item.Key, item.Value))
                return CreatedAtAction(nameof(Read), new { key = item.Key }, item);
            else
                return Conflict("An item with the same key already exists.");
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
