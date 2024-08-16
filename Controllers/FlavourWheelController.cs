﻿using Microsoft.AspNetCore.Mvc;
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
            _context.FlavourWheels.RemoveRange(_context.FlavourWheels);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
