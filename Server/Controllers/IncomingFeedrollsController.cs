using MES.Server.Contracts;
using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomingFeedrollsController : ControllerBase
    {
        private readonly IIncomingFeedrollsRepository _incomingFeedrollsRepository;
        private readonly ProjectdbContext _context;
        public IncomingFeedrollsController(IIncomingFeedrollsRepository incomingFeedrollsRepository, ProjectdbContext context)
        {
            _incomingFeedrollsRepository = incomingFeedrollsRepository;
            _context = context;
        }
        [HttpGet("{serialNumber}")]
        public async Task<ActionResult<IncomingInspectionFeedRolls>> GetById(string serialNumber)
        {
            var result = await _incomingFeedrollsRepository.GetByIdAsync(serialNumber);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("adddata")]
        public async Task<ActionResult<IncomingInspectionFeedRolls>> Add(IncomingInspectionFeedRolls model)
        {
            var exists = await _incomingFeedrollsRepository.SerialNumberExistsAsync(model.SerialNumber);
            if (exists) return Conflict("Serial number already exists.");

            var created = await _incomingFeedrollsRepository.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { serialNumber = created.SerialNumber }, created);



            //try
            //{
            //    // _context.RotorsFinalInspections.Add(rotor);
            //    // Add the inspection to the database
            //    _context.IncomingInspectionFeedRollsdata.Add(model);

            //    // Generate PDF from the incoming data
            //  //  byte[] pdfBytes = GeneratePDF(rotor);

            //    // Send email with PDF as attachment
            //  //  await SendEmailWithAttachment(pdfBytes, rotor);

            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            //}
            //catch (Exception ex)
            //{
            //    // Log the error (optional)
            //    Console.WriteLine($"Error occurred: {ex.Message}");
            //    return StatusCode(500, "Internal server error.");
            //}
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _incomingFeedrollsRepository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}