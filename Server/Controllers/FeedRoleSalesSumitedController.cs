using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedRoleSalesSumitedController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public FeedRoleSalesSumitedController(ProjectdbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<IncomingInspectionFeedRolls>>> GetAll()
        {
            var records = await _context.IncomingInspectionFeedRollsdata.ToListAsync();

            if (records == null || !records.Any())
                return NotFound("No inspection records found.");

            return Ok(records);
        }
    }
}
