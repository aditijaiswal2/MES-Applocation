using MES.Server.Data;
using MES.Shared.Entities;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Dialog.Grinding.GrindingData;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorWaitingSalesClearanceVC;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorSalesClearanceController : ControllerBase
    {
        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RotorSalesClearanceController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }




        [HttpPost("AddRotorSalesClearance")]
        public async Task<IActionResult> AffRotorSalesClearance([FromBody] SalesClearenceSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorSalesClearance
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
                    Workcenters = submission.SelectedInspection.Workcenters,
                    AdvancedSharpingStatus = submission.SelectedInspection.AdvancedSharpingStatus,
                    ProductionSubmitDate = submission.SelectedInspection.ProductionSubmitDate,
                    ProductionSubmitBy = submission.SelectedInspection.ProductionSubmitBy,
                    RotorsDiaLeft = submission.SelectedInspection.RotorsDiaLeft,
                    RotorsDiaRight = submission.SelectedInspection.RotorsDiaRight,
                    ReliefLand = submission.SelectedInspection.ReliefLand,
                    ToothFaceLeft = submission.SelectedInspection.ToothFaceLeft,
                    ToothFaceRight = submission.SelectedInspection.ToothFaceRight,
                    CentersLeft = submission.SelectedInspection.CentersLeft,
                    CentersRight = submission.SelectedInspection.CentersRight,
                    VisualChecks = submission.SelectedInspection.VisualChecks,
                    InspectedBy = submission.SelectedInspection.InspectedBy,
                    Notes = submission.SelectedInspection.Notes,
                    GrindingStartDate = submission.SelectedInspection.GrindingStartDate,
                    DelayReasonTracking = submission.SelectedInspection.DelayReasonTracking,
                   GrindingSubmiteddBy = submission.SelectedInspection.GrindingSubmiteddBy,
                   GrindingEndDate = submission.SelectedInspection.GrindingEndDate,
                   CustomerPoNum = submission.SelectedInspection.CustomerPoNum,
                   DWGNum = submission.SelectedInspection.DWGNum,
                   AGNum = submission.SelectedInspection.AGNum,
                   SpecialNoteComment = submission.SelectedInspection.SpecialNoteComment,
                    Dressedwithnewbearing = submission.SelectedInspection.Dressedwithnewbearing,
                    InspectorSing = submission.SelectedInspection.InspectorSing,
                    Description = submission.SelectedInspection.Description,
                    Oktoship = submission.SelectedInspection.Oktoship,
                    InspectorComments = submission.SelectedInspection.InspectorComments,
                    Start = submission.SelectedInspection.Start,
                    Finish = submission.SelectedInspection.Finish,
                    Name = submission.SelectedInspection.Name,
                    Date = submission.SelectedInspection.Date,
                    FinalInspectionSubmiteddBy = submission.SelectedInspection.FinalInspectionSubmiteddBy,
                    FinalInspectionSubmitedByDate = submission.SelectedInspection.FinalInspectionSubmitedByDate,
                    AdditionalWSalesComments = submission.AdditionalComments,
                    WSalesSubmitedByDate = submission.SubmitDate,
                    WSalesSubmiteddBy = submission.SubmitedBy
                };

                _context.RotorSalesClearance.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotors Sales Clearance data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor sales clearance data: {ex.Message}");
            }
        }

        [HttpGet("GetAllSalesClearanceData")]
        public async Task<ActionResult<IEnumerable<RotorSalesClearance>>> GetAllSalesClearanceData()
        {
            var records = await _context.RotorSalesClearance.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No Rotor Sales Clearance Data records found.");

            return Ok(records);
        }


    }
}
