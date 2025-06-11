using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Dialog.Grinding.GrindingData;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorGrindingVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorGrindingSavedController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorGrindingSavedController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddGrindingSaveData")]
        public async Task<IActionResult> AddGrindingSaveData([FromBody] GrindingStartdataSubmission submission)
        {
            if (submission == null || submission.SelectedProductionInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorGrindingSavedData
                {
                    SerialNumber = submission.SelectedProductionInspection.SerialNumber,
                    Module = submission.SelectedProductionInspection.Module,
                    SalesOrderNumber = submission.SelectedProductionInspection.SalesOrderNumber,
                    WorkOrder = submission.SelectedProductionInspection.WorkOrder,
                    MatNumber = submission.SelectedProductionInspection.MatNumber,
                    Customer = submission.SelectedProductionInspection.Customer, 
                    RotorsNumber = submission.SelectedProductionInspection.RotorsNumber,
                    RotorCategorization = submission.SelectedProductionInspection.RotorCategorization,
                    ComponentType = submission.SelectedProductionInspection.ComponentType, 
                    TargetDate = submission.SelectedProductionInspection.TargetDate,
                    CustomerImportance = submission.SelectedProductionInspection.CustomerImportance,                   
                    Workcenters = submission.SelectedProductionInspection.Workcenters,
                    AdvancedSharpingStatus = submission.SelectedProductionInspection.AdvancedSharpingStatus, 
                    GrindingStartDate = submission.GrindingStartDate,
                    IsStarted = submission.IsStarted,
                    IsSecondaryWorkCenters = submission.IsSecondaryWorkCenters,
                    SecondaryWorkCenters = submission.SecondaryWorkCenters,
                    GrindingdataSecondaryWorkCentersSubmitedByDate = submission.GrindingdataSecondaryWorkCentersSubmitedByDate,
                    GrindingdataSecondaryWorkCentersSubmiteddBy = submission.GrindingdataSecondaryWorkCentersSubmiteddBy,
                    GrindingdataSavedBy = submission.GrindingdataSavedBy,
                    GrindingdataSavedByDate = DateTime.Now.ToString(),

                };

                _context.RotorGrindingSavedData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Grinding data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Grinding data: {ex.Message}");
            }
        }


        [HttpPost("UpdateGrindingSaveData")]
        public async Task<IActionResult> UpdateGrindingSaveData([FromBody] GrindingStartdataSubmission updateSubmission)
        {
            if (updateSubmission == null || string.IsNullOrEmpty(updateSubmission.SelectedProductionInspection.SerialNumber))
                return BadRequest("Update submission is invalid.");

            try
            {
                // Get the latest data for this SerialNumber based on GrindingdataSavedByDate
                var existingData = await _context.RotorGrindingSavedData
                    .Where(x => x.SerialNumber == updateSubmission.SelectedProductionInspection.SerialNumber)
                    .OrderByDescending(x => x.GrindingdataSavedByDate)
                    .FirstOrDefaultAsync();

                if (existingData == null)
                    return NotFound($"Rotor Grinding data not found for Serial Number: {updateSubmission.SelectedProductionInspection.SerialNumber}");

                // Update only the necessary fields
                existingData.IsStarted = updateSubmission.IsStarted;
                existingData.SecondaryWorkCenters = updateSubmission.SecondaryWorkCenters;
                existingData.IsSecondaryWorkCenters = updateSubmission.IsSecondaryWorkCenters;
                existingData.GrindingdataSecondaryWorkCentersSubmiteddBy = updateSubmission.GrindingdataSecondaryWorkCentersSubmiteddBy;
                existingData.GrindingdataSecondaryWorkCentersSubmitedByDate = updateSubmission.GrindingdataSecondaryWorkCentersSubmitedByDate;

                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Grinding data updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating rotor Grinding data: {ex.Message}");
            }
        }


        [HttpGet("GGSDBSN/{serialNumber}")]
        public async Task<ActionResult<IEnumerable<RotorGrindingSavedData>>> GetBySerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return BadRequest("Serial number is required.");

            var records = await _context.RotorGrindingSavedData
                .Where(i => i.SerialNumber == serialNumber)
                .ToListAsync();

            if (records == null || records.Count == 0)
                return NotFound($"No records found for Serial Number: {serialNumber}");

            return Ok(records);
        }


        [HttpGet("GetAllSavedGrindingData")]
        public async Task<ActionResult<IEnumerable<RotorGrindingSavedData>>> GetAllSavedGrindingData()
        {
            var records = await _context.RotorGrindingSavedData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No Rotor Grinding records found.");

            return Ok(records);
        }

    }
}
