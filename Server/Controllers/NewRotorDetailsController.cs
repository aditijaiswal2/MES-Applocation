using MES.Server.Data;
using MES.Shared.Models;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRotorDetailsController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public NewRotorDetailsController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveNewRotor")]
        public async Task<IActionResult> SaveNewRotor([FromBody] NewRotorData rotorData)
        {
            if (rotorData == null) return BadRequest("Invalid data");

            _context.NewRotorData.Add(rotorData); 
            await _context.SaveChangesAsync();

            return Ok(rotorData);
        }

        [HttpGet("GetLatestSerialNumber")]
        public async Task<IActionResult> GetLatestSerialNumber()
        {
            var latestRotor = await _context.NewRotorData
                .OrderByDescending(r => r.NewRotorDataSubmitDate)  // or SerialNumber if needed
                .FirstOrDefaultAsync();

            if (latestRotor == null)
                return Ok(""); // no data yet

            return Ok(latestRotor.SerialNumber);
        }

        [HttpGet("GetAllNewRotorData")]
        public async Task<ActionResult<IEnumerable<NewRotorData>>> GetAllNewRotorData()
        {
            var records = await _context.NewRotorData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No New Rotor Data records found.");

            return Ok(records);
        }

        // update the isdelete status in receive page 
        [HttpPut("updateRIsdelete")]
        public async Task<IActionResult> UpdateReceivedeletedType([FromBody] Receiving receiving)
        {
            var rotor = await _context.Receivings
                .FirstOrDefaultAsync(r => r.SerialNumber == receiving.SerialNumber);

            if (rotor == null)
            {
                return NotFound("Rotor with specified serial number not found.");
            }

            rotor.IsDeleted = true;

            _context.Receivings.Update(rotor);
            await _context.SaveChangesAsync();

            return Ok("Rotor updated to 'IsDeleted'.");
        }


        // update the isdelete status in new rotor page 
        [HttpPut("UpNewISDelete")]
        public async Task<IActionResult> UpdateNewRotorDeletedType([FromBody] Receiving receiving)
        {
            var rotor = await _context.NewRotorData
                .FirstOrDefaultAsync(r => r.SerialNumber == receiving.SerialNumber);

            if (rotor == null)
            {
                return NotFound("Rotor with specified serial number not found.");
            }

            rotor.IsDeleted = true;

            _context.NewRotorData.Update(rotor);
            await _context.SaveChangesAsync();

            return Ok("Rotor updated to 'IsDeleted'.");
        }



    }
}
