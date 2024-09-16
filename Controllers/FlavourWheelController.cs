using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flavour_Wheel_Server.Data;
using Flavour_Wheel_Server.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Flavour_Wheel_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlavourWheelController : ControllerBase
    {
        private readonly FlavourWheelContext _context;
        private readonly ILogger<FlavourWheelController> _logger;

        public FlavourWheelController(FlavourWheelContext context, ILogger<FlavourWheelController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/flavourwheel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlavourWheel>>> GetAll()
        {
            try
            {
                return await _context.FlavourWheels.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all FlavourWheels");
                return StatusCode(500, "Internal server error occurred while retrieving data");
            }
        }

        // GET: api/flavourwheel/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlavourWheel>> GetById(int id)
        {
            try
            {
                var flavourWheel = await _context.FlavourWheels.FindAsync(id);

                if (flavourWheel == null)
                {
                    return NotFound($"FlavourWheel with id {id} not found");
                }

                return flavourWheel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting FlavourWheel with id {id}");
                return StatusCode(500, "Internal server error occurred while retrieving data");
            }
        }

        // POST: api/flavourwheel
        [HttpPost]
        public async Task<ActionResult<FlavourWheel>> Create(FlavourWheel flavourWheel)
        {
            try
            {
                _logger.LogInformation($"Attempting to create FlavourWheel: {JsonSerializer.Serialize(flavourWheel)}");

                _context.FlavourWheels.Add(flavourWheel);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"FlavourWheel created successfully with id: {flavourWheel.Id}");

                return CreatedAtAction(nameof(GetById), new { id = flavourWheel.Id }, flavourWheel);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "A database error occurred while creating the FlavourWheel");
                return StatusCode(500, "A database error occurred while saving the entity");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the FlavourWheel");
                return StatusCode(500, "Internal server error occurred while creating the entity");
            }
        }

        // PUT: api/flavourwheel/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FlavourWheel flavourWheel)
        {
            if (id != flavourWheel.Id)
            {
                return BadRequest("Id in URL does not match Id in the data");
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
                    return NotFound($"FlavourWheel with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating FlavourWheel with id {id}");
                return StatusCode(500, "Internal server error occurred while updating the entity");
            }

            return NoContent();
        }

        // DELETE: api/flavourwheel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var flavourWheel = await _context.FlavourWheels.FindAsync(id);
                if (flavourWheel == null)
                {
                    return NotFound($"FlavourWheel with id {id} not found");
                }

                _context.FlavourWheels.Remove(flavourWheel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting FlavourWheel with id {id}");
                return StatusCode(500, "Internal server error occurred while deleting the entity");
            }
        }

        // DELETE: api/flavourwheel
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM FlavourWheels;");
                await _context.Database.ExecuteSqlRawAsync("ALTER TABLE FlavourWheels AUTO_INCREMENT = 1;");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting all FlavourWheels");
                return StatusCode(500, "Internal server error occurred while deleting all entities");
            }
        }

        private bool FlavourWheelExists(int id)
        {
            return _context.FlavourWheels.Any(e => e.Id == id);
        }
    }
}