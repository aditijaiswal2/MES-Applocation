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
                    Received = submission.SelectedInspection.Received,
                    Inspected = submission.SelectedInspection.Inspected,
                    RotorsNumber = submission.SelectedInspection.RotorsNumber,
                    RotorsDia = submission.SelectedInspection.RotorsDia,
                    RotorCategorization = submission.SelectedInspection.RotorCategorization,
                    ComponentType = submission.SelectedInspection.ComponentType,
                    DateTime = submission.SelectedInspection.DateTime,
                    TargetDate = submission.SelectedInspection.TargetDate,
                    CustomerImportance = submission.SelectedInspection.CustomerImportance,
                    Workcenters = submission.SelectedInspection.Workcenters,
                    AdvancedSharpingStatus = submission.SelectedInspection.AdvancedSharpingStatus,
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

        [HttpGet("GetSCDatabySerialNo/{serialNumber}")]
        public async Task<IActionResult> GetSCDatabySerialNo(string serialNumber)
        {
            var data = await _context.RotorsFinalInspections
                .Where(r => r.SerialNumber.ToLower() == serialNumber.ToLower())
                .ToListAsync();

            if (data == null || data.Count == 0)
                return NotFound("No data found for the given serial number.");

            return Ok(data);
        }

    }
}
