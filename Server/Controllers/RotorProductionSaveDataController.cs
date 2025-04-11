using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorProductionSchedulingVC;


namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorProductionSaveDataController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorProductionSaveDataController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddProductionSaveData")]
        public async Task<IActionResult> AddProductionSaveData([FromBody] productiondataSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorProductionSavedData
                {
                    SerialNumber = submission.SelectedInspection.SerialNumber,
                    Module = submission.SelectedInspection.Module,
                    SalesOrderNumber = submission.SelectedInspection.SalesOrderNumber,
                    WorkOrder = submission.SelectedInspection.WorkOrder,
                    MatNumber = submission.SelectedInspection.MatNumber,
                    Customer = submission.SelectedInspection.Customer,
                    Location = submission.SelectedInspection.Location,
                    Received = submission.SelectedInspection.Received,
                    Inspected = submission.SelectedInspection.Inspected,
                    RotorsNumber = submission.SelectedInspection.RotorsNumber,
                    Initials = submission.SelectedInspection.Initials,
                    Make = submission.SelectedInspection.Make,
                    Dia = submission.SelectedInspection.Dia,
                    Len = submission.SelectedInspection.Len,
                    Fits = submission.SelectedInspection.Fits,
                    Materials = submission.SelectedInspection.Materials,
                    Others = submission.SelectedInspection.Others,
                    RotorsDia = submission.SelectedInspection.RotorsDia,
                    RotorStyle = submission.SelectedInspection.RotorStyle,
                    Type = submission.SelectedInspection.Type,
                    BearingRemoved = submission.SelectedInspection.BearingRemoved,
                    Bearing = submission.SelectedInspection.Bearing,
                    BearingSeals = submission.SelectedInspection.BearingSeals,
                    CeramicSeals = submission.SelectedInspection.CeramicSeals,
                    Right = submission.SelectedInspection.Right,
                    yRight = submission.SelectedInspection.yRight,
                    Left = submission.SelectedInspection.Left,
                    yLeft = submission.SelectedInspection.yLeft,
                    BasicSharpening = submission.SelectedInspection.BasicSharpening,
                    IfYBasicSharpening = submission.SelectedInspection.IfYBasicSharpening,
                    WedgelockAlignmentMarks = submission.SelectedInspection.WedgelockAlignmentMarks,
                    CenterGrinding = submission.SelectedInspection.CenterGrinding,
                    IfYCenterGrinding = submission.SelectedInspection.IfYCenterGrinding,
                    Aligned = submission.SelectedInspection.Aligned,
                    PlasticSleaves = submission.SelectedInspection.PlasticSleaves,
                    Welding = submission.SelectedInspection.Welding,
                    WeldingNum = submission.SelectedInspection.WeldingNum,
                    BedKnife = submission.SelectedInspection.BedKnife,
                    BoxReceivedWithSaddles = submission.SelectedInspection.BoxReceivedWithSaddles,
                    ReProfile = submission.SelectedInspection.ReProfile,
                    SandBlasting = submission.SelectedInspection.SandBlasting,
                    ManualLabor = submission.SelectedInspection.ManualLabor,
                    Bottom = submission.SelectedInspection.Bottom,
                    Top = submission.SelectedInspection.Top,
                    AddQty = submission.SelectedInspection.AddQty,
                    TirLeftJournal = submission.SelectedInspection.TirLeftJournal,
                    TirRightJournal = submission.SelectedInspection.TirRightJournal,
                    SaddlePartNumber = submission.SelectedInspection.SaddlePartNumber,
                    RotorCategorization = submission.SelectedInspection.RotorCategorization,
                    ComponentType = submission.SelectedInspection.ComponentType,
                    Users = submission.SelectedInspection.Users,
                    DateTime = submission.SelectedInspection.DateTime,
                    TargetDate = submission.SelectedInspection.TargetDate,
                    CustomerInstructions = submission.SelectedInspection.CustomerInstructions,
                    CustomerImportance = submission.SelectedInspection.CustomerImportance,
                    SubmitDate = submission.SelectedInspection.DateTime,
                    SubmitedBy = submission.SelectedInspection.Users,
                   Workcenters = submission.Workcenters ?? "N/A",
                    AdvancedSharpingStatus = submission.AdvancedSharpingStatus,
                   ProductionSavedDate = submission.ProductionSavedDate,
                   ProductionSavedBy = submission.ProductionSavedBy,                 


                };

                _context.RotorProductionSavedData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor sales data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor sales data: {ex.Message}");
            }
        }


        [HttpGet("GetRecentProductionSaveData")]
        public async Task<IActionResult> GetRecentSalesData(string serialNumber, string module, string rotorsNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber) || string.IsNullOrWhiteSpace(module) || string.IsNullOrWhiteSpace(rotorsNumber))
                return BadRequest("Invalid parameters provided.");

            try
            {
                var recentData = await _context.RotorProductionSavedData
                    .Where(r =>
                        r.SerialNumber == serialNumber &&
                        r.Module == module &&
                        r.RotorsNumber == rotorsNumber)
                    .OrderByDescending(r => r.ProductionSavedDate)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NotFound("No matching rotor production save data found.");

                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor production save data: {ex.Message}");
            }
        }


    }
}
