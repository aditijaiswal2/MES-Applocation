using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using MES.Server.Data;
using MES.Shared.Entities;
using MES.Shared.Models;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Drawing;
using System.Reflection;
using static MES.Client.Pages.Rotor_FeedRolls_Service.FinalInspectionVC;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorIncomingInspectionVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorsFinalInspectionsaveController : Controller
    {
        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RotorsFinalInspectionsaveController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("AddFinalSaveData")]
        public async Task<IActionResult> AddSalesSaveData([FromBody] FinalInspectionSubmit submission)
        {

            try
            {
                var rotorData = new FinalInspectionSaveData
                {
                    SerialNumber = submission.SerialNumber,
                    Module = submission.Module,
                    SalesOrderNumber = submission.SalesOrderNumber,
                    WorkOrder = submission.WorkOrder,
                    MatNumber = submission.MatNumber,
                    Customer = submission.Customer,
                    Location = submission.Location,
                    Received = submission.Received,
                    Inspected = submission.Inspected,
                    Len = submission.Len,
                    RotorsNumber = submission.RotorsNumber,
                    FluteDiameterStart = submission.RotorsDia,
                    FluteDiameterFinish = submission.FluteDiameterFinish,
                    LandWidthFinish = submission.LandWidthFinish,
                    LandWidthStart = submission.LandWidthStart,
                    ReliefAngleStart = submission.ReliefAngleStart,
                    ReliefAngleFinish = submission.ReliefAngleFinish,
                    TIRStart = submission.CentersLeft,
                    TIRfinish = submission.CentersRight,
                    TaperStart = submission.TaperStart,
                    Taperfinish = submission.Taperfinish,
                    LocknutThreadsStart = submission.LocknutThreadsStart,
                    IstheRotorcleanStart = submission.IstheRotorcleanStart,
                    JournalsOKStart = submission.JournalsOKStart,
                    WedgelockassemblyStart = submission.WedgelockassemblyStart,
                    SpecialPartWashStart = submission.SpecialPartWashStart,
                    InspectorSing = submission.InspectorSing,
                    InspectorComments = submission.InspectorComments,
                    Oktoship = submission.Oktoship,
                    Initials = submission.Initials,
                    Make = submission.Make,
                    Dia = submission.Dia,
                    RotorStyle = submission.RotorStyle,
                    Fits = submission.Fits,
                    Materials = submission.Materials,
                    Others = submission.Others,
                    RotorsDia = submission.RotorsDia,
                    Type = submission.Type,
                    BearingRemoved = submission.BearingRemoved,
                    Bearing = submission.Bearing,
                    BearingSeals = submission.BearingSeals,
                    CeramicSeals = submission.CeramicSeals,
                    Right = submission.Right,
                    yRight = submission.yRight,
                    yLeft = submission.yLeft,
                    Left = submission.Left,
                    BasicSharpening = submission.BasicSharpening,
                    IfYBasicSharpening = submission.IfYBasicSharpening,
                    WedgelockAlignmentMarks = submission.WedgelockAlignmentMarks,
                    CenterGrinding = submission.CenterGrinding,
                    IfYCenterGrinding = submission.IfYCenterGrinding,
                    Aligned = submission.Aligned,
                    PlasticSleaves = submission.PlasticSleaves,
                    Welding = submission.Welding,
                    WeldingNum = submission.WeldingNum,
                    BedKnife = submission.BedKnife,
                    BoxReceivedWithSaddles = submission.BoxReceivedWithSaddles,
                    ReProfile = submission.ReProfile,
                    SandBlasting = submission.SandBlasting,
                    ManualLabor = submission.ManualLabor,
                    Bottom = submission.Bottom,
                    Top = submission.Top,
                    AddQty = submission.AddQty,
                    TirLeftJournal = submission.TirLeftJournal,
                    TirRightJournal = submission.TirRightJournal,
                    SaddlePartNumber = submission.SaddlePartNumber,
                    RotorCategorization = submission.RotorCategorization,
                    ComponentType = submission.ComponentType,
                    Users = submission.Users,
                    TargetDate = submission.TargetDate,
                    CustomerInstructions = submission.CustomerInstructions,
                    CustomerImportance = submission.CustomerImportance,
                    SubmitDate = submission.SubmitDate,
                    SubmitedBy = submission.SubmitedBy,
                    AdvancedSharpingStatus = submission.AdvancedSharpingStatus,
                    Workcenters = submission.Workcenters,
                    ProductionSubmitDate = submission.ProductionSubmitDate,
                    ProductionSubmitBy = submission.ProductionSubmitBy,
                    RotorsDiaLeft = submission.RotorsDiaLeft,
                    RotorsDiaRight = submission.RotorsDiaRight,
                    ReliefLand = submission.ReliefLand,
                    ToothFaceLeft = submission.ToothFaceLeft,
                    ToothFaceRight = submission.ToothFaceRight,
                    CentersLeft = submission.CentersLeft,
                    CentersRight = submission.CentersRight,
                    VisualChecks = submission.VisualChecks,
                    InspectedBy = submission.Inspected,
                    GrindingStartDate = submission.GrindingStartDate,
                    Notes = submission.Notes,
                    DelayReasonTracking = submission.DelayReasonTracking,
                    CustomerPoNum = submission.CustomerPoNum,
                    DWGNum = submission.DWGNum,
                    AGNum = submission.AGNum,
                    SpecialNoteComment = submission.SpecialNoteComment,
                    Dressedwithnewbearing = submission.Dressedwithnewbearing,
                    Description = submission.Description,
                    Name = submission.Name,
                    FinalInspectionSubmiteddBy = submission.Users,
                    FinalInspectionSubmitedByDate = DateTime.Now,

                };

                _context.finalInspectionSaveDatas.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Final Incoming Inspection data saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving rotor Incoming Inspection data: {ex.Message}");
            }
        }


        [HttpGet("GetRecentIncomingData")]
        public async Task<IActionResult> GetRecentSalesData(string serialNumber, string module)
        {


            try
            {
                var recentData = await _context.finalInspectionSaveDatas
                    .Where(r =>
                        r.SerialNumber == serialNumber &&
                        r.Module == module)
                    .OrderByDescending(r => r.DateTime)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NotFound("No matching Final Incoming Inspection data found.");

                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor Incoming Inspection data: {ex.Message}");
            }
        }


        [HttpPut("UpdateRotorIISavedData")]
        public async Task<IActionResult> UpdateRotorData([FromBody] FinalInspectionSubmit data)
        {
            var existing = await _context.finalInspectionSaveDatas.FirstOrDefaultAsync(x => x.SerialNumber == data.SerialNumber && x.Module == data.Module);

            if (existing == null) return NotFound();
            existing.Customer = data.Customer;
            existing.SerialNumber = data.SerialNumber;
            existing.Module = data.Module;
            existing.SalesOrderNumber = data.SalesOrderNumber;
            existing.WorkOrder = data.WorkOrder;
            existing.MatNumber = data.MatNumber;
            existing.Location = data.Location;
            existing.Received = data.Received;
            existing.Inspected = data.Inspected;
            existing.Len = data.Len;
            existing.RotorsNumber = data.RotorsNumber;
            existing.FluteDiameterStart = data.FluteDiameterStart;
            existing.FluteDiameterFinish = data.FluteDiameterFinish;
            existing.LandWidthFinish = data.LandWidthFinish;
            existing.LandWidthStart = data.LandWidthStart;
            existing.ReliefAngleStart = data.ReliefAngleStart;
            existing.ReliefAngleFinish = data.ReliefAngleFinish;
            existing.TIRStart = data.TIRStart;
            existing.TIRfinish = data.TIRfinish;
            existing.TaperStart = data.TaperStart;
            existing.Taperfinish = data.Taperfinish;
            existing.LocknutThreadsStart = data.LocknutThreadsStart;
            existing.IstheRotorcleanStart = data.IstheRotorcleanStart;
            existing.JournalsOKStart = data.JournalsOKStart;
            existing.WedgelockassemblyStart = data.WedgelockassemblyStart;
            existing.SpecialPartWashStart = data.SpecialPartWashStart;
            existing.InspectorSing = data.InspectorSing;
            existing.InspectorComments = data.InspectorComments;
            existing.Oktoship = data.Oktoship;
            existing.Initials = data.Initials;
            existing.Make = data.Make;
            existing.Dia = data.Dia;
            existing.RotorStyle = data.RotorStyle;
            existing.Fits = data.Fits;
            existing.Materials = data.Materials;
            existing.Others = data.Others;
            existing.RotorsDia = data.RotorsDia;
            existing.Type = data.Type;
            existing.BearingRemoved = data.BearingRemoved;
            existing.Bearing = data.Bearing;
            existing.BearingSeals = data.BearingSeals;
            existing.CeramicSeals = data.CeramicSeals;
            existing.Right = data.Right;
            existing.yRight = data.yRight;
            existing.Left = data.Left;
            existing.yLeft = data.yLeft;
            existing.BasicSharpening = data.BasicSharpening;
            existing.IfYBasicSharpening = data.IfYBasicSharpening;
            existing.WedgelockAlignmentMarks = data.WedgelockAlignmentMarks;
            existing.CenterGrinding = data.CenterGrinding;
            existing.IfYCenterGrinding = data.IfYCenterGrinding;
            existing.Aligned = data.Aligned;
            existing.PlasticSleaves = data.PlasticSleaves;
            existing.Welding = data.Welding;
            existing.WeldingNum = data.WeldingNum;
            existing.BedKnife = data.BedKnife;
            existing.ReProfile = data.ReProfile;
            existing.SandBlasting = data.SandBlasting;
            existing.ManualLabor = data.ManualLabor;
            existing.Bottom = data.Bottom;
            existing.Top = data.Top;
            existing.AddQty = data.AddQty;
            existing.TirLeftJournal = data.TirLeftJournal;
            existing.TirRightJournal = data.TirRightJournal;
            existing.SaddlePartNumber = data.SaddlePartNumber;
            existing.RotorCategorization = data.RotorCategorization;
            existing.ComponentType = data.ComponentType;
            existing.Users = data.Users;
            existing.TargetDate = data.TargetDate;
            existing.CustomerInstructions = data.CustomerInstructions;
            existing.CustomerImportance = data.CustomerImportance;
            existing.SubmitDate = data.SubmitDate;
            existing.SubmitedBy = data.SubmitedBy;
            existing.AdvancedSharpingStatus = data.AdvancedSharpingStatus;
            existing.Workcenters = data.Workcenters;
            existing.ProductionSubmitDate = data.ProductionSubmitDate;
            existing.ProductionSubmitBy = data.ProductionSubmitBy;
            existing.RotorsDiaLeft = data.RotorsDiaLeft;
            existing.RotorsDiaRight = data.RotorsDiaRight;
            existing.ReliefLand = data.ReliefLand;
            existing.InspectedBy = data.InspectedBy;
            existing.VisualChecks = data.VisualChecks;
            existing.CentersRight = data.CentersRight;
            existing.CentersLeft = data.CentersLeft;
            existing.ToothFaceRight = data.ToothFaceRight;
            existing.ToothFaceLeft = data.ToothFaceLeft;
            existing.Description = data.Description;
            existing.Dressedwithnewbearing = data.Dressedwithnewbearing;
            existing.SpecialNoteComment = data.SpecialNoteComment;
            existing.AGNum = data.AGNum;
            existing.DWGNum = data.DWGNum;
            existing.CustomerPoNum = data.CustomerPoNum;
            existing.DelayReasonTracking = data.DelayReasonTracking;
            existing.Notes = data.Notes;
            existing.GrindingStartDate = data.GrindingStartDate;
            existing.FinalInspectionSubmitedByDate = DateTime.Now;
            existing.FinalInspectionSubmiteddBy = data.FinalInspectionSubmiteddBy;
            existing.Name = data.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("CheckIfRotorExists")]
        public async Task<IActionResult> CheckIfRotorExists(string serialNumber, string module)
        {
            var exists = await _context.finalInspectionSaveDatas.AnyAsync(x => x.SerialNumber == serialNumber && x.Module == module);
            return Ok(exists);
        }

    }
}
