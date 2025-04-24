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
                    Location = submission.SelectedProductionInspection.Location,
                    Received = submission.SelectedProductionInspection.Received,
                    Inspected = submission.SelectedProductionInspection.Inspected,
                    RotorsNumber = submission.SelectedProductionInspection.RotorsNumber,
                    Initials = submission.SelectedProductionInspection.Initials,
                    Make = submission.SelectedProductionInspection.Make,
                    Dia = submission.SelectedProductionInspection.Dia,
                    Len = submission.SelectedProductionInspection.Len,
                    Fits = submission.SelectedProductionInspection.Fits,
                    Materials = submission.SelectedProductionInspection.Materials,
                    Others = submission.SelectedProductionInspection.Others,
                    RotorsDia = submission.SelectedProductionInspection.RotorsDia,
                    RotorStyle = submission.SelectedProductionInspection.RotorStyle,
                    Type = submission.SelectedProductionInspection.Type,
                    BearingRemoved = submission.SelectedProductionInspection.BearingRemoved,
                    Bearing = submission.SelectedProductionInspection.Bearing,
                    BearingSeals = submission.SelectedProductionInspection.BearingSeals,
                    CeramicSeals = submission.SelectedProductionInspection.CeramicSeals,
                    Right = submission.SelectedProductionInspection.Right,
                    yRight = submission.SelectedProductionInspection.yRight,
                    Left = submission.SelectedProductionInspection.Left,
                    yLeft = submission.SelectedProductionInspection.yLeft,
                    BasicSharpening = submission.SelectedProductionInspection.BasicSharpening,
                    IfYBasicSharpening = submission.SelectedProductionInspection.IfYBasicSharpening,
                    WedgelockAlignmentMarks = submission.SelectedProductionInspection.WedgelockAlignmentMarks,
                    CenterGrinding = submission.SelectedProductionInspection.CenterGrinding,
                    IfYCenterGrinding = submission.SelectedProductionInspection.IfYCenterGrinding,
                    Aligned = submission.SelectedProductionInspection.Aligned,
                    PlasticSleaves = submission.SelectedProductionInspection.PlasticSleaves,
                    Welding = submission.SelectedProductionInspection.Welding,
                    WeldingNum = submission.SelectedProductionInspection.WeldingNum,
                    BedKnife = submission.SelectedProductionInspection.BedKnife,
                    BoxReceivedWithSaddles = submission.SelectedProductionInspection.BoxReceivedWithSaddles,
                    ReProfile = submission.SelectedProductionInspection.ReProfile,
                    SandBlasting = submission.SelectedProductionInspection.SandBlasting,
                    ManualLabor = submission.SelectedProductionInspection.ManualLabor,
                    Bottom = submission.SelectedProductionInspection.Bottom,
                    Top = submission.SelectedProductionInspection.Top,
                    AddQty = submission.SelectedProductionInspection.AddQty,
                    TirLeftJournal = submission.SelectedProductionInspection.TirLeftJournal,
                    TirRightJournal = submission.SelectedProductionInspection.TirRightJournal,
                    SaddlePartNumber = submission.SelectedProductionInspection.SaddlePartNumber,
                    RotorCategorization = submission.SelectedProductionInspection.RotorCategorization,
                    ComponentType = submission.SelectedProductionInspection.ComponentType,
                    Users = submission.SelectedProductionInspection.Users,
                    DateTime = submission.SelectedProductionInspection.DateTime,
                    TargetDate = submission.SelectedProductionInspection.TargetDate,
                    CustomerInstructions = submission.SelectedProductionInspection.CustomerInstructions,
                    CustomerImportance = submission.SelectedProductionInspection.CustomerImportance,
                    SubmitDate = submission.SelectedProductionInspection.DateTime,
                    SubmitedBy = submission.SelectedProductionInspection.Users,
                    Workcenters = submission.SelectedProductionInspection.Workcenters,
                    AdvancedSharpingStatus = submission.SelectedProductionInspection.AdvancedSharpingStatus,
                    ProductionSubmitDate = submission.SelectedProductionInspection.ProductionSubmitDate,
                    ProductionSubmitBy = submission.SelectedProductionInspection.ProductionSubmitBy,
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


    }
}
