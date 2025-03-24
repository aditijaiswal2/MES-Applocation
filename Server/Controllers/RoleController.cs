using MES.Server.Controllers;
using MES.Shared.DTOs;
using MES.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MES.Server.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /**
         * Gets the list of roles
         *
         * @return json
         **/
        //[Authorize(Roles = "Admin")]
        [HttpGet("getrole")]
        public async Task<ActionResult> FetchRoles()
        {
            var roles = await _roleManager.Roles
            .Select(r => new
            {
                id = r.Id,
                name = r.Name,
                listItems = r.ListItems,
                readOnly = r.ReadOnly
            })
            .ToListAsync();

            return Ok(roles);
        }

        /**
         * Adds a new role
         *
         * @param RoleAddDto roleDto
         * @return json
         **/
        //[Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<ActionResult> AddRole(RoleAddDto roleDto)
        {
            var role = await _roleManager.Roles.SingleOrDefaultAsync(r => r.NormalizedName == roleDto.Name.ToUpper());
            if (EqualityComparer<AppRole>.Default.Equals(role, default(AppRole)))
            {
                var newRole = new AppRole { Name = roleDto.Name, ListItems = roleDto.WcList, ReadOnly = roleDto.ReadOnly };
                await _roleManager.CreateAsync(newRole);
                return Created("Created", new { Id = newRole.Id, Name = newRole.Name, ListItems = newRole.ListItems, ReadOnly = newRole.ReadOnly });
            }
            else
            {
                //Updated
                role.ListItems = roleDto.WcList;
                role.ReadOnly = roleDto.ReadOnly;
                await _roleManager.UpdateAsync(role);
                return Ok(new { Id = role.Id, Name = role.Name, ListItems = role.ListItems, ReadOnly = role.ReadOnly });
            }
        }
        [HttpPut("editrole/{Id}")]
        public async Task<ActionResult> EditRole(int Id, RoleAddDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            role.Name = roleDto.Name;
            role.ListItems = roleDto.WcList;
            role.ReadOnly = roleDto.ReadOnly;

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update role");
            }

            return Ok(new { Id = role.Id, Name = role.Name, ListItems = role.ListItems, ReadOnly = role.ReadOnly });
        }


        [HttpDelete("deleterole/{Id}")]
        public async Task<ActionResult> DeleteRole(int Id)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete role");
            }

            return NoContent();
        }



    }
}

