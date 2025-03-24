using MES.Server.Contracts;
using MES.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReceivingDataController : ControllerBase
{
    private readonly IReceivingDataRepository _repository;

    public ReceivingDataController(IReceivingDataRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getrd")]
    public async Task<ActionResult<IEnumerable<Receiving>>> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Receiving>> GetById(int id)
    {
        var data = await _repository.GetByIdAsync(id);
        if (data == null)
            return NotFound();
        return Ok(data);
    }

    [HttpPost("addrd")]
    public async Task<ActionResult<Receiving>> Create([FromBody] Receiving data)
    {
        if (data == null) return BadRequest();

        var createdData = await _repository.CreateAsync(data);
        return CreatedAtAction(nameof(GetById), new { id = createdData.Id }, createdData);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Receiving data)
    {
        if (data == null || id != data.Id)
            return BadRequest();

        bool updated = await _repository.UpdateAsync(data);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
   
    {
        bool deleted = await _repository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
