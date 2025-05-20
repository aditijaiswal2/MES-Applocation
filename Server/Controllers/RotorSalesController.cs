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
    public class RotorSalesController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorSalesController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpGet("GetBySerialNumber/{serialNumber}")]
        public async Task<ActionResult<IEnumerable<RotorSalesData>>> GetBySerialNumber(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                return BadRequest("Serial number is required.");

            var records = await _context.RotorSalesData
                .Where(i => i.SerialNumber == serialNumber)
                .ToListAsync();

            if (records == null || records.Count == 0)
                return NotFound($"No records found for Serial Number: {serialNumber}");

            return Ok(records);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<IncomingInspection>>> GetAll()
        {
            var records = await _context.IncomingInspections.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No inspection records found.");

            return Ok(records);
        }

        [HttpPost("AddSalesData")]
        public async Task<IActionResult> AddSalesData([FromBody] InspectionSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorSalesData
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
                    WeldingNum = submission.SelectedInspection.WeldingNum?.ToString(), // fixed
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
                    ADDQTYdata = submission.SelectedInspection.ADDQTYdata?.ToString(), 
                    NewBoxRequired = submission.SelectedInspection.NewBoxRequired,
                    NewBoxRequiredBox = submission.SelectedInspection.NewBoxRequiredBox,
                    TargetDate = submission.TargetDate,
                    PlannedHours = submission.PlannedHours,
                    CustomerInstructions = submission.CustomerInstructions,
                    CustomerImportance = submission.CustomerPriority,
                    SubmitDate = submission.SubmitDate,
                    SubmitedBy = submission.SubmitedBy,
                   
                };

                _context.RotorSalesData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor sales data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor sales data: {ex.Message}");
            }
        }

        [HttpGet("GetAllSalesData")]
        public async Task<ActionResult<IEnumerable<RotorSalesData>>> GetAllRotorSalesData()
        {
            var records = await _context.RotorSalesData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No RotorSalesData records found.");

            return Ok(records);
        }


    }
}
