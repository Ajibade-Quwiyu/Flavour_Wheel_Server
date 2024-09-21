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
                var flavourWheels = await _context.FlavourWheels.ToListAsync();
                _logger.LogInformation($"Retrieved {flavourWheels.Count} FlavourWheel entries");
                return flavourWheels;
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
                    _logger.LogWarning($"FlavourWheel with id {id} not found");
                    return NotFound($"FlavourWheel with id {id} not found");
                }

                _logger.LogInformation($"Retrieved FlavourWheel with id {id}");
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
            try
            {
                var existingFlavourWheel = await _context.FlavourWheels.FindAsync(id);

                if (existingFlavourWheel == null)
                {
                    _logger.LogInformation($"FlavourWheel with id {id} not found. Creating new entry.");
                    flavourWheel.Id = 0; // Ensure a new entry is created
                    _context.FlavourWheels.Add(flavourWheel);
                }
                else
                {
                    _context.Entry(existingFlavourWheel).CurrentValues.SetValues(flavourWheel);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"FlavourWheel with id {flavourWheel.Id} updated/created successfully");
                return Ok(flavourWheel);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning($"Concurrency error occurred while updating FlavourWheel with id {id}");
                return StatusCode(409, "The entity has been modified by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating/creating FlavourWheel with id {id}");
                return StatusCode(500, "Internal server error occurred while updating/creating the entity");
            }
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
                    _logger.LogWarning($"FlavourWheel with id {id} not found for deletion");
                    return NotFound($"FlavourWheel with id {id} not found");
                }

                _context.FlavourWheels.Remove(flavourWheel);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"FlavourWheel with id {id} deleted successfully");
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Attempting to delete all FlavourWheel entries");
                
                // Delete all entries
                await _context.FlavourWheels.ExecuteDeleteAsync();
                
                // Reset the ID counter (this approach should work for most database providers)
                var sqlCommand = _context.Database.IsSqlServer() 
                    ? "DBCC CHECKIDENT ('FlavourWheels', RESEED, 0)" 
                    : "ALTER TABLE FlavourWheels AUTO_INCREMENT = 1";
                await _context.Database.ExecuteSqlRawAsync(sqlCommand);

                await transaction.CommitAsync();
                
                _logger.LogInformation("All FlavourWheel entries deleted and ID reset successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting all FlavourWheels");
                return StatusCode(500, $"Internal server error occurred while deleting all entities: {ex.Message}");
            }
        }

        private bool FlavourWheelExists(int id)
        {
            return _context.FlavourWheels.Any(e => e.Id == id);
        }
    }
}