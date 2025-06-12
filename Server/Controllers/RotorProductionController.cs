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
    public class RotorProductionController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorProductionController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddProductionData")]
        public async Task<IActionResult> AddSalesData([FromBody] productiondataSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorProductionData
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
                    Workcenters = submission.Workcenters,
                    AdvancedSharpingStatus = submission.AdvancedSharpingStatus,
                    ProductionSubmitDate = submission.ProductionSavedDate,
                    ProductionSubmitBy = submission.ProductionSavedBy,
                };

                _context.RotorProductionData.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Production data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Production data: {ex.Message}");
            }
        }


        [HttpGet("GetAllProductionData")]
        public async Task<ActionResult<IEnumerable<RotorProductionData>>> GetAllRotorProductionData()
        {
            var records = await _context.RotorProductionData.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No RotorProductionData records found.");

            return Ok(records);
        }


        [HttpGet("GetISPDatabySerialNo/{serialNumber}")]
        public async Task<IActionResult> GetISPDatabySerialNo(string serialNumber)
        {
            var data = await _context.RotorProductionData
                .Where(r => r.SerialNumber.ToLower() == serialNumber.ToLower())
                .ToListAsync();

            if (data == null || data.Count == 0)
                return NotFound("No data found for the given serial number.");

            return Ok(data);
        }

    }
}
