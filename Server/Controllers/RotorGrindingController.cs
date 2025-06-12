using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Dialog.Grinding.GrindingData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorGrindingController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorGrindingController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddGrindingData")]
        public async Task<IActionResult> AddSalesData([FromBody] GrindingInspectionSubmission submission)
        {
            if (submission == null || submission.SelectedProductionInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorGrindingData
                {
                    SerialNumber = submission.SelectedProductionInspection.SerialNumber,
                    Module = submission.SelectedProductionInspection.Module,
                    SalesOrderNumber = submission.SelectedProductionInspection.SalesOrderNumber,
                    WorkOrder = submission.SelectedProductionInspection.WorkOrder,
                    MatNumber = submission.SelectedProductionInspection.MatNumber,
                    Customer = submission.SelectedProductionInspection.Customer,                   
                    RotorsNumber = submission.SelectedProductionInspection.RotorsNumber,                  
                    Materials = submission.SelectedProductionInspection.Materials,                   
                    RotorsDia = submission.SelectedProductionInspection.RotorsDia,                 
                    RotorCategorization = submission.SelectedProductionInspection.RotorCategorization,
                    ComponentType = submission.SelectedProductionInspection.ComponentType, 
                    Users = submission.SelectedProductionInspection.Users,
                    DateTime = submission.SelectedProductionInspection.DateTime,
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
                    GrindingdataSubmiteddBy = submission.GrindingdataSavedBy,
                    GrindingdataSubmitedByDate = submission.GrindingdataSavedByDate
                    
                };

                _context.RotorGrindingData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Grinding data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Grinding data: {ex.Message}");
            }
        }


        [HttpGet("GetAllGrindingData")]
        public async Task<ActionResult<IEnumerable<RotorGrindingData>>> GetAllRotorGrindingData()
        {
            var records = await _context.RotorGrindingData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No Rotor Grinding records found.");

            return Ok(records);
        }

        [HttpGet("{serialNumber}")]
        public async Task<IActionResult> GetSerialData(string serialNumber)
        {
            var data = await _context.RotorGrindingData
                .FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpGet("CheckSerialExists/{serialNumber}")]
        public async Task<IActionResult> CheckSerialExists(string serialNumber)
        {
            var exists = await _context.RotorGrindingData
                .AnyAsync(r => r.SerialNumber == serialNumber); // adjust property name if different
            return Ok(exists);
        }


        [HttpGet("GetGrindingDatabySerialNo/{serialNumber}")]
        public async Task<IActionResult> GetGrindingDatabySerialNo(string serialNumber)
        {
            var data = await _context.RotorGrindingData
                .Where(r => r.SerialNumber.ToLower() == serialNumber.ToLower())
                .ToListAsync();

            if (data == null || data.Count == 0)
                return NotFound("No data found for the given serial number.");

            return Ok(data);
        }

    }
}
