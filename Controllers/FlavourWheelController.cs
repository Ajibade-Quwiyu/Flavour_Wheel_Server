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

                var existingFlavourWheel = await _context.FlavourWheels.FirstOrDefaultAsync(fw => fw.Username == flavourWheel.Username);
                if (existingFlavourWheel != null)
                {
                    _logger.LogWarning($"FlavourWheel with username {flavourWheel.Username} already exists");
                    return Conflict($"FlavourWheel with username {flavourWheel.Username} already exists");
                }

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
                    // Check if a FlavourWheel with the same username already exists
                    var duplicateUsername = await _context.FlavourWheels.FirstOrDefaultAsync(fw => fw.Username == flavourWheel.Username);
                    if (duplicateUsername != null)
                    {
                        _logger.LogWarning($"FlavourWheel with username {flavourWheel.Username} already exists");
                        return Conflict($"FlavourWheel with username {flavourWheel.Username} already exists");
                    }

                    _logger.LogInformation($"FlavourWheel with id {id} not found. Creating new entry.");
                    flavourWheel.Id = 0; // Ensure a new entry is created
                    _context.FlavourWheels.Add(flavourWheel);
                }
                else
                {
                    // Check if updating the username would create a duplicate
                    if (existingFlavourWheel.Username != flavourWheel.Username)
                    {
                        var duplicateUsername = await _context.FlavourWheels.FirstOrDefaultAsync(fw => fw.Username == flavourWheel.Username && fw.Id != id);
                        if (duplicateUsername != null)
                        {
                            _logger.LogWarning($"Cannot update: FlavourWheel with username {flavourWheel.Username} already exists");
                            return Conflict($"FlavourWheel with username {flavourWheel.Username} already exists");
                        }
                    }

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
            // ... (unchanged)
        }

        // DELETE: api/flavourwheel
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            // ... (unchanged)
        }

        // GET: api/flavourwheel/byusername/{username}
        [HttpGet("byusername/{username}")]
        public async Task<ActionResult<FlavourWheel>> GetByUsername(string username)
        {
            try
            {
                var flavourWheel = await _context.FlavourWheels.FirstOrDefaultAsync(fw => fw.Username == username);

                if (flavourWheel == null)
                {
                    _logger.LogWarning($"FlavourWheel with username {username} not found");
                    return NotFound($"FlavourWheel with username {username} not found");
                }

                _logger.LogInformation($"Retrieved FlavourWheel with username {username}");
                return flavourWheel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting FlavourWheel with username {username}");
                return StatusCode(500, "Internal server error occurred while retrieving data");
            }
        }
    }
}