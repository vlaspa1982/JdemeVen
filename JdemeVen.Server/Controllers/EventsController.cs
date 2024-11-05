using JdemeVen.Server.Data;
using JdemeVen.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JdemeVen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
           var eventById = await _context.Events.FindAsync(id);
            if (eventById == null)
            {
                return NotFound();
            }
            return Ok(eventById); 
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event ev)
        {
            if (ev == null)
            {
                return BadRequest();
            }
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventById), ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event e)
        {
            if (id != e.Id)
            {
                return BadRequest();
            }

            _context.Events.Update(e);
            await _context.SaveChangesAsync();
            return Ok(e);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventById(int id) { 
            var e = await _context.Events.FindAsync(id);

            if(e == null)
            {
                return NotFound();
            }

            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return Ok(e);
        }

    }

}
