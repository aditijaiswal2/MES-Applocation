using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Pages.Rotor_FeedRolls_Service.DamageDuringGrindingRotorsVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorDamageGrindingSavedController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorDamageGrindingSavedController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddDamageGSDDSaveData")]
        public async Task<IActionResult> AddDDGRSaveData([FromBody] damagegrindingSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorDamageGrindingSaveData
                {
                    SerialNumber = submission.SelectedInspection.SerialNumber,
                    Module = submission.SelectedInspection.Module,
                    SalesOrderNumber = submission.SelectedInspection.SalesOrderNumber,
                    WorkOrder = submission.SelectedInspection.WorkOrder,
                    MatNumber = submission.SelectedInspection.MatNumber,
                    Customer = submission.SelectedInspection.Customer,                   
                    RotorsNumber = submission.SelectedInspection.RotorsNumber,
                    RotorCategorization = submission.SelectedInspection.RotorCategorization,
                    ComponentType = submission.SelectedInspection.ComponentType,
                    Workcenters = submission.Workcenters ?? "N/A",
                    AdvancedSharpingStatus = submission.AdvancedSharpingStatus,
                    DamageGrindingSavedDate = submission.DamageGrindingSavedDate,
                    DamageGrindingSavedBy = submission.DamageGrindingSavedBy,
                };

                _context.RotorDamageGrindingSaveData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Damage Grinding data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Damage Grinding data: {ex.Message}");
            }
        }


        [HttpGet("GetRecentDDGSaveData")]
        public async Task<IActionResult> GetRecentSalesData(string serialNumber, string module, string rotorsNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber) || string.IsNullOrWhiteSpace(module) || string.IsNullOrWhiteSpace(rotorsNumber))
                return BadRequest("Invalid parameters provided.");

            try
            {
                var recentData = await _context.RotorDamageGrindingSaveData
                    .Where(r =>
                        r.SerialNumber == serialNumber &&
                        r.Module == module &&
                        r.RotorsNumber == rotorsNumber)
                    .OrderByDescending(r => r.DamageGrindingSavedDate)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NotFound("No matching rotor damage save data found.");

                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor damage save data: {ex.Message}");
            }
        }

        [HttpGet("GetAllGrindingData")]
        public async Task<ActionResult<IEnumerable<RotorDamageGrindingSaveData>>> GetAllRotorGrindingData()
        {
            var records = await _context.RotorDamageGrindingSaveData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No Rotor Grinding records found.");

            return Ok(records);
        }
    }
}
