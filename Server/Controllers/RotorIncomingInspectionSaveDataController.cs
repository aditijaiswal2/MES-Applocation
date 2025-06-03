using Azure;
using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorIncomingInspectionVC;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorSalesVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotorIncomingInspectionSaveDataController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public RotorIncomingInspectionSaveDataController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("AddIncomingSaveData")]
        public async Task<IActionResult> AddSalesSaveData([FromBody] IncomingInspectionSubmit submission)
        {
           
            try
            {
                var rotorData = new RotorIncominInspectionSavedData
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
                    RotorsNumber = submission.RotorsNumber,
                    Initials = submission.Initials,
                    Make = submission.Make,
                    Dia = submission.Dia,
                    Len = submission.Len,
                    Fits = submission.Fits,
                    Materials = submission.Materials,
                    Others = submission.Others,
                    RotorsDia = submission.RotorsDia,
                    RotorStyle = submission.RotorStyle,
                    Type = submission.Type,
                    BearingRemoved = submission.BearingRemoved,
                    Bearing = submission.Bearing,
                    BearingSeals = submission.BearingSeals,
                    CeramicSeals = submission.CeramicSeals,
                    Right = submission.Right,
                    yRight = submission.yRight,
                    Left = submission.Left,
                    yLeft = submission.yLeft,
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
                    DateTime = submission.DateTime,
                    ADDQTYdata = submission.ADDQTYdata,
                    NewBoxRequired = submission.NewBoxRequired,
                    NewBoxRequiredBox = submission.NewBoxRequiredBox,
                 
                };

                _context.rotorIncominInspectionSavedDatas.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Rotor Incoming Inspection data saved successfully!" });
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
                var recentData = await _context.rotorIncominInspectionSavedDatas
                    .Where(r =>
                        r.SerialNumber == serialNumber &&
                        r.Module == module)
                    .OrderByDescending(r => r.DateTime)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NotFound("No matching rotor Incoming Inspection data found.");

                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor Incoming Inspection data: {ex.Message}");
            }
        }


        [HttpPut("UpdateRotorIISavedData")]
        public async Task<IActionResult> UpdateRotorData([FromBody] IncomingInspectionSubmit data)
        {
            var existing = await _context.rotorIncominInspectionSavedDatas.FirstOrDefaultAsync(x => x.SerialNumber == data.SerialNumber && x.Module == data.Module);

            if (existing == null) return NotFound();

            // Map fields from data to existing entity
            existing.Customer = data.Customer;
            existing.Location = data.Location;
          
            existing.RotorsNumber = data.RotorsNumber;
       
            existing.Make = data.Make;
            existing.Dia = data.Dia;
            existing.Len = data.Len;

            existing.Fits = data.Fits;
            existing.Materials = data.Materials;
            existing.Others = data.Others;
            existing.RotorsDia = data.RotorsDia;
            existing.RotorStyle = data.RotorStyle;
            existing.Type = data.Type;
            existing.BearingRemoved = data.BearingRemoved;
            existing.Bearing = data.Bearing;
            existing.BearingSeals = data.BearingSeals;
            existing.CeramicSeals = data.CeramicSeals;
            existing.Left = data.Left;
            existing.yLeft = data.yLeft;
            existing.Right = data.Right;
            existing.yRight = data.yRight;
            existing.BasicSharpening = data.BasicSharpening;
            existing.IfYBasicSharpening = data.IfYBasicSharpening;
            existing.WedgelockAlignmentMarks = data.WedgelockAlignmentMarks;
            existing.CenterGrinding = data.CenterGrinding;
            existing.IfYCenterGrinding = data.IfYCenterGrinding;
            existing.Aligned = data.Aligned;
            existing.PlasticSleaves = data.PlasticSleaves;
            existing.Welding = data.Welding;
            existing.WeldingNum = data.WeldingNum;
            existing.BedKnife =     data.BedKnife;
            existing.BoxReceivedWithSaddles = data.BoxReceivedWithSaddles;
            existing.ADDQTYdata = data.ADDQTYdata;
            existing.ReProfile = data.ReProfile;
            existing.ManualLabor = data.ManualLabor;
            existing.NewBoxRequired = data.NewBoxRequired;
            existing.NewBoxRequiredBox = data.NewBoxRequiredBox;
            existing.Top = data.Top;
            existing.Bottom = data.Bottom;
            existing.AddQty = data.AddQty;
            existing.SaddlePartNumber = data.SaddlePartNumber;
            existing.RotorCategorization = data.RotorCategorization;
            existing.ComponentType = data.ComponentType;
            // ... all other fields
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("CheckIfRotorExists")]
        public async Task<IActionResult> CheckIfRotorExists(string serialNumber, string module)
        {
            var exists = await _context.rotorIncominInspectionSavedDatas.AnyAsync(x => x.SerialNumber == serialNumber && x.Module == module);
            return Ok(exists);
        }


        [HttpGet("GetRecentIncomingsavedDatabasedonserialnumber/{serialNumber}")]       
        public async Task<IActionResult> GetRecentIncomingsavedDatabasedonserialnumber(string serialNumber)
        {
            try
            {
                var recentData = await _context.rotorIncominInspectionSavedDatas
                    .Where(r =>
                        r.SerialNumber == serialNumber)
                    .OrderByDescending(r => r.DateTime)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NoContent(); 


                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor Incoming Inspection data: {ex.Message}");
            }
        }


        [HttpGet("GetRecentIncomingsabmitDatabasedonserialnumber/{serialNumber}")]       
        public async Task<IActionResult> GetRecentIncomingsabmitDatabasedonserialnumber(string serialNumber)
        {
            try
            {
                var recentData = await _context.IncomingInspections
                    .Where(r =>
                        r.SerialNumber == serialNumber)
                    .OrderByDescending(r => r.DateTime)
                    .FirstOrDefaultAsync();

                if (recentData == null)
                    return NoContent(); 


                return Ok(recentData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving rotor Incoming Inspection data: {ex.Message}");
            }
        }


    }
}
