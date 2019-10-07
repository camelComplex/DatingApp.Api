using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsMessagesController : ControllerBase
    {
        private readonly DataContext _context;

        public NewsMessagesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/NewsMessages
        [HttpGet]
        public IEnumerable<NewsMessage> GetNewsMessages()
        {
            return _context.NewsMessages;
        }

        // GET: api/NewsMessages/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsMessage = await _context.NewsMessages.SingleOrDefaultAsync(m => m.Id == id);

            if (newsMessage == null)
            {
                return NotFound();
            }

            return Ok(newsMessage);
        }

        // PUT: api/NewsMessages/5
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsMessage([FromRoute] int id, [FromBody] NewsMessage newsMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NewsMessages
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostNewsMessage([FromBody] NewsMessage newsMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NewsMessages.Add(newsMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsMessage", new { id = newsMessage.Id }, newsMessage);
        }

        // DELETE: api/NewsMessages/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsMessage = await _context.NewsMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (newsMessage == null)
            {
                return NotFound();
            }

            _context.NewsMessages.Remove(newsMessage);
            await _context.SaveChangesAsync();

            return Ok(newsMessage);
        }

        private bool NewsMessageExists(int id)
        {
            return _context.NewsMessages.Any(e => e.Id == id);
        }

    }
}