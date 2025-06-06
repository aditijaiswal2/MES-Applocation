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
    public class RotorGrindingSavedController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorGrindingSavedController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddGrindingSaveData")]
        public async Task<IActionResult> AddGrindingSaveData([FromBody] GrindingInspectionSubmission submission)
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
                    RotorsDiaLeft = submission.RotorsDiaLeft,
                    RotorsDiaRight = submission.RotorsDiaRight,
                    ReliefLand = submission.ReliefLand,
                    ToothFaceLeft = submission.ToothFaceLeft,
                    ToothFaceRight = submission.ToothFaceRight,
                    CentersLeft = submission.CentersLeft,
                    CentersRight = submission.CentersRight,
                    VisualChecks = submission.VisualChecks,
                    InspectedBy = submission.InspectedBy,
                    Notes = submission.Notes,
                    GrindingStartDate = submission.GrindingStartDate,
                    DelayReasonTracking = submission.DelayReasonTracking,
                    AdditionalSalesComments = submission.AdditionalSalesComments,
                    IsMoveoutsideoperation = submission.IsMoveoutsideoperation,
                    GrindingdataSavedBy = submission.GrindingdataSavedBy,
                    GrindingdataSavedByDate = submission.GrindingdataSavedByDate,                    
                    
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
