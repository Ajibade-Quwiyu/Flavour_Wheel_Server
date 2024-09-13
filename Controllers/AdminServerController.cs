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
    public class AdminServerController : ControllerBase
    {
        private readonly FlavourWheelContext _context;

        public AdminServerController(FlavourWheelContext context)
        {
            _context = context;
        }

        // GET: api/adminserver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminServer>>> GetAll()
        {
            return await _context.AdminServers.ToListAsync();
        }

        // GET: api/adminserver/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminServer>> GetById(int id)
        {
            var adminServer = await _context.AdminServers.FindAsync(id);

            if (adminServer == null)
            {
                return NotFound();
            }

            return adminServer;
        }

        // POST: api/adminserver
        [HttpPost]
        public async Task<ActionResult<AdminServer>> Create(AdminServer adminServer)
        {
            _context.AdminServers.Add(adminServer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = adminServer.Id }, adminServer);
        }

        // PUT: api/adminserver/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdminServer adminServer)
        {
            if (id != adminServer.Id)
            {
                return BadRequest();
            }

            _context.Entry(adminServer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminServerExists(id))
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

        // DELETE: api/adminserver/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var adminServer = await _context.AdminServers.FindAsync(id);
            if (adminServer == null)
            {
                return NotFound();
            }

            _context.AdminServers.Remove(adminServer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/adminserver
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM AdminServers;");
            await _context.Database.ExecuteSqlRawAsync("ALTER TABLE AdminServers AUTO_INCREMENT = 0;");
            return NoContent();
        }

        private bool AdminServerExists(int id)
        {
            return _context.AdminServers.Any(e => e.Id == id);
        }
    }
}