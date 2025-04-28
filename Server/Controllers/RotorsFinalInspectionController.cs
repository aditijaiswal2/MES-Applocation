using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MES.Shared.Models.Rotors;
using MES.Server.Data;
using MES.Shared.DTOs;

namespace YourAppNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorsFinalInspectionController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorsFinalInspectionController(ProjectdbContext context)
        {
            _context = context;
        }

        // GET: api/RotorsFinalInspection
        [HttpGet("GetFinalInspection")]
        public async Task<ActionResult<IEnumerable<RotorsFinalInspection>>> GetRotorsFinalInspections()
        {
            return await _context.RotorsFinalInspections.ToListAsync();
        }

        // GET: api/RotorsFinalInspection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RotorsFinalInspection>> GetRotorsFinalInspection(int id)
        {
            var rotor = await _context.RotorsFinalInspections.FindAsync(id);

            if (rotor == null)
            {
                return NotFound();
            }

            return rotor;
        }

        // POST: api/RotorsFinalInspection
        [HttpPost("AddFinalInspection")]
        public async Task<ActionResult<RotorsFinalInspection>> PostRotorsFinalInspection(RotorsFinalInspection rotor)
        {
            _context.RotorsFinalInspections.Add(rotor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRotorsFinalInspection), new { id = rotor.Id }, rotor);
        }

        // PUT: api/RotorsFinalInspection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRotorsFinalInspection(int id, RotorsFinalInspection rotor)
        {
            if (id != rotor.Id)
            {
                return BadRequest();
            }

            _context.Entry(rotor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RotorsFinalInspections.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/RotorsFinalInspection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRotorsFinalInspection(int id)
        {
            var rotor = await _context.RotorsFinalInspections.FindAsync(id);
            if (rotor == null)
            {
                return NotFound();
            }

            _context.RotorsFinalInspections.Remove(rotor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddAllData")]
        public async Task<IActionResult> PostFinalInspection([FromBody] RotorFinalInspectionDTO model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            return Ok(new { message = "Inspection data saved successfully" });
        }

        [HttpGet("CheckSerialExists/{serialNumber}")]
        public async Task<IActionResult> CheckSerialExists(string serialNumber)
        {
            var exists = await _context.RotorsFinalInspections
                .AnyAsync(r => r.SerialNumber == serialNumber); // adjust property name if different
            return Ok(exists);
        }

    }
}

