using MES.Server.Data;
using MES.Shared.Entities;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static MES.Client.Pages.Rotor_FeedRolls_Service.RotorWaitingSalesClearanceVC;
using static MES.Client.Pages.ShippingVC;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ShippingController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("Ship")]
        public async Task<IActionResult> AffRotorSalesClearance([FromBody] ShippingSubmission submission)
        {
            if (submission == null || submission.SelectedInspection == null)
                return BadRequest("Submission is invalid.");

            try
            {
                var rotorData = new RotorShipping
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
                    RotorCategorization = submission.SelectedInspection.RotorCategorization,
                    ComponentType = submission.SelectedInspection.ComponentType,
                    DateTime = submission.SelectedInspection.DateTime,
                    TargetDate = submission.SelectedInspection.TargetDate,
                    CustomerImportance = submission.SelectedInspection.CustomerImportance,
                    Workcenters = submission.SelectedInspection.Workcenters,
                    AdvancedSharpingStatus = submission.SelectedInspection.AdvancedSharpingStatus,
                    AdditionalWSalesComments = submission.SelectedInspection.AdditionalWSalesComments,
                    ShipSubmiteddBy = submission.SubmitedBy,
                    ShipSubmitedByDate = submission.SubmitDate
                };

                _context.RotorShipping.Add(rotorData);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "data shipped successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error shipped data: {ex.Message}");
            }
        }

        [HttpGet("GetAllShippedData")]
        public async Task<ActionResult<IEnumerable<RotorShipping>>> GetAllSalesClearanceData()
        {
            var records = await _context.RotorShipping.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No shipped Data records found.");

            return Ok(records);
        }


    }
}
