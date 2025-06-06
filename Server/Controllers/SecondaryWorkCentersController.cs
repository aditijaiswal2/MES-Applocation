using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Dialog.Grinding.GrindingData;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondaryWorkCentersController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public SecondaryWorkCentersController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddSWData")]
        public async Task<IActionResult> AddecondaryWorkCentersData([FromBody] SecondaryWorkCentersSubmission submission)
        {
            if (submission == null || submission.SelectedProductionInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorGrindingSecondaryWorkCentersData
                {
                    SerialNumber = submission.SelectedProductionInspection.SerialNumber,
                    Module = submission.SelectedProductionInspection.Module,                    
                    RotorsNumber = submission.SelectedProductionInspection.RotorsNumber,
                    Workcenters = submission.SelectedProductionInspection.Workcenters,                    
                    GrindingStartDate = submission.GrindingStartDate,
                    IsMoveoutsideoperation = submission.IsMoveoutsideoperation,
                    IsSecondaryWorkCenters = submission.IsSecondaryWorkCenters,
                    SecondaryWorkCenters = submission.SecondaryWorkCenters,
                    GrindingdataSubmiteddBy = submission.GrindingdataSavedBy,
                    GrindingdataSubmitedByDate = submission.GrindingdataSavedByDate

                };

                _context.RotorGrindingSecondaryWorkCentersData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Secondary WorkCenters data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Secondary WorkCenters data: {ex.Message}");
            }
        }


        [HttpGet("GetAllSWData")]
        public async Task<ActionResult<IEnumerable<RotorGrindingSecondaryWorkCentersData>>> GetAllsecondaryWorkCentersData()
        {
            var records = await _context.RotorGrindingSecondaryWorkCentersData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No Rotor Secondary WorkCenters records found.");

            return Ok(records);
        }

        [HttpGet("GSW/{serialNumber}")]
        public async Task<IActionResult> GetRecentData(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return BadRequest("Serial number is required.");

            try
            {
                var records = await _context.RotorGrindingSecondaryWorkCentersData
                    .Where(i => i.SerialNumber == serialNumber)
                    .ToListAsync();

                if (records.Count == 0)
                    return NotFound("No matching data found.");

                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving data: {ex.Message}");
            }
        }


    }
}
