using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorSalesVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorSalesSaveDataController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorSalesSaveDataController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddSalesSaveData")]
        public async Task<IActionResult> AddSalesSaveData([FromBody] InspectionSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorSalesSavedData
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
                    WeldingNum = submission.SelectedInspection.WeldingNum?.ToString(),
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
                    ADDQTYdata = submission.SelectedInspection.ADDQTYdata?.ToString(),
                   
                    NewBoxRequired = submission.SelectedInspection.NewBoxRequired,
                    NewBoxRequiredBox = submission.SelectedInspection.NewBoxRequiredBox,
                    DateTime = submission.SelectedInspection.DateTime,
                    TargetDate = submission.TargetDate,
                    PlannedHours = submission.PlannedHours,
                    CustomerInstructions = submission.CustomerInstructions,
                    CustomerImportance = submission.CustomerPriority,
                    SavedDate = submission.SubmitDate,
                    SavedBy = submission.SubmitedBy,
                };

                _context.RotorSalesSavedData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor sales data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor sales data: {ex.Message}");
            }
        }


        [HttpGet("GetRecentSalesData")]
        public async Task<IActionResult> GetRecentSalesData(string serialNumber, string module, string rotorsNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber) || string.IsNullOrWhiteSpace(module) || string.IsNullOrWhiteSpace(rotorsNumber))
                return BadRequest("Invalid parameters provided.");

            try
            {
                var recentData = await _context.RotorSalesSavedData
                    .Where(r =>
                        r.SerialNumber == serialNumber &&
                        r.Module == module &&
                        r.RotorsNumber == rotorsNumber)
                    .OrderByDescending(r => r.SavedDate)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NotFound("No matching rotor sales data found.");

                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor sales data: {ex.Message}");
            }
        }


    }
}
