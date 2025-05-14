using MES.Server.Data.Repositories;
using MES.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewBoxRequiredNumberController : BaseApiController
    {
        private readonly NewBoxRequiredNumberRepository _rotorsStyleService;

        public NewBoxRequiredNumberController(NewBoxRequiredNumberRepository rotorsStyleService)
        {
            _rotorsStyleService = rotorsStyleService;
        }

        [HttpPost("addwc")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Addworkcenters([FromBody] NewBoxRequiredNumber rotors)
        {
            if (rotors == null)
            {
                return BadRequest();
            }
            try
            {
                var createrotorsStyle = await _rotorsStyleService.AddWorkCenterAsync(rotors);

                if (!createrotorsStyle)
                {
                    return BadRequest();
                }

                return Ok("New Box Required Number added successfully.");

            }
            catch (Exception ex) { return StatusCode(500, $"An error occurred: {ex.Message}"); }
        }


        [HttpPut("editwc")]
        public async Task<IActionResult> Updateworkcenters([FromBody] NewBoxRequiredNumber rotors)
        {
            await _rotorsStyleService.EditWorkCenterAsync(rotors);
            return Ok(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteworkcenters(int id)
        {
            await _rotorsStyleService.DeleteWorkCenter(id);
            return Ok(200);
        }

        [HttpGet("getwc")]
        public async Task<ActionResult<IEnumerable<NewBoxRequiredNumber>>> Getworkcenters()
        {
            var result = await _rotorsStyleService.GetWorkCenterAsync();
            return Ok(result);
        }
    }
}
