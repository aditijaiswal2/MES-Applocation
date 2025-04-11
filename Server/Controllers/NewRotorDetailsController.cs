using MES.Server.Data;
using MES.Shared.Models.Rotors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRotorDetailsController : ControllerBase
    {
        private readonly ProjectdbContext _context;

        public NewRotorDetailsController(ProjectdbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveNewRotor")]
        public async Task<IActionResult> SaveNewRotor([FromBody] NewRotorData rotorData)
        {
            if (rotorData == null) return BadRequest("Invalid data");

            _context.NewRotorData.Add(rotorData); 
            await _context.SaveChangesAsync();

            return Ok(rotorData);
        }

    }
}
