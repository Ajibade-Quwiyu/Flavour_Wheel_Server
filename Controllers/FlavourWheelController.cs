using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flavour_Wheel_Server.Data;
using Flavour_Wheel_Server.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flavour_Wheel_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavourWheelController : ControllerBase
    {
        private readonly FlavourWheelContext _context;

        public FlavourWheelController(FlavourWheelContext context)
        {
            _context = context;
        }

        // GET: api/flavourwheel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlavourWheel>>> GetAll()
        {
            return await _context.FlavourWheels.ToListAsync();
        }

        // GET: api/flavourwheel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlavourWheel>> GetById(int id)
        {
            var flavourWheel = await _context.FlavourWheels.FindAsync(id);

            if (flavourWheel == null)
            {
                return NotFound();
            }

            return flavourWheel;
        }

        // POST: api/flavourwheel
        [HttpPost]
        public async Task<ActionResult<FlavourWheel>> Create(FlavourWheel flavourWheel)
        {
            _context.FlavourWheels.Add(flavourWheel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = flavourWheel.Id }, flavourWheel);
        }

        // PUT: api/flavourwheel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FlavourWheel flavourWheel)
        {
            if (id != flavourWheel.Id)
            {
                return BadRequest();
            }

            _context.Entry(flavourWheel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlavourWheelExists(id))
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

        // DELETE: api/flavourwheel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var flavourWheel = await _context.FlavourWheels.FindAsync(id);
            if (flavourWheel == null)
            {
                return NotFound();
            }

            _context.FlavourWheels.Remove(flavourWheel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/flavourwheel
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM FlavourWheels;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE FlavourWheels AUTO_INCREMENT = 0;");
            return NoContent();
        }

        private bool FlavourWheelExists(int id)
        {
            return _context.FlavourWheels.Any(e => e.Id == id);
        }
    }
}