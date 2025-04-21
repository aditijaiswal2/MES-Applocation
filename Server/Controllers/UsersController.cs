using MES.Server.Data;
using MES.Shared.DTOs;
using MES.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ProjectdbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(ProjectdbContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        /**
         * Gets a list of users for Admin
         *
         * @return json
         **/
        //[Authorize(Roles = "Admin")]
        [HttpGet("getuser")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Where(u => u.isDeleted == 0)
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Usercode = u.UserCode,
                    Name = u.Name,
                    Email = u.Email,
                    PageNames = u.PageNames,
                    SelectedWorkCenter = u.SelectedWorkCenter,
                    isSalesuser = u.IsSalesUser,
                    Routes = u.Routes,

                })
                .ToListAsync();

            return Ok(users);
        }


        [HttpGet("getuserroles/{userId}")]
        public IActionResult GetUserRoles(int userId)
        {
            var roles = _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.Name)
                .ToList();

            // Join the roles into a single string separated by commas
            string rolesString = string.Join(", ", roles);

            return Ok(rolesString);
        }

        /**
         * Gets the user details
         *
         * @param string username
         * @return json
         **/
        // [Authorize(Roles = "Admin")]
        [HttpGet("{username}")]
        public async Task<ActionResult> GetUser(string username)
        {
            var user = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Name = u.Name,
                    Email = u.Email,

                    PageNames = u.PageNames,
                    SelectedWorkCenter = u.SelectedWorkCenter,
                    Routes = u.Routes,

                })
                .SingleOrDefaultAsync(u => u.Username == username.ToLower());

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }


        public async Task<string> GenerateUserCode()
        {
            int lastUserCodeNumber = 1000; // Default start number

            // Fetch the latest user with a valid UserCode format
            var lastUser = await _userManager.Users
                .Where(u => u.UserCode != null && u.UserCode.StartsWith("C-"))
                .OrderByDescending(u => u.Id) // Use Id for guaranteed uniqueness
                .FirstOrDefaultAsync();

            if (lastUser != null && !string.IsNullOrWhiteSpace(lastUser.UserCode))
            {
                // Extract numeric part using regex
                var match = System.Text.RegularExpressions.Regex.Match(lastUser.UserCode, @"\d+");

                if (match.Success && int.TryParse(match.Value, out int lastNumber))
                {
                    lastUserCodeNumber = lastNumber + 1;
                }
            }

            return $"C-{lastUserCodeNumber}";
        }


        /**
         * Add a new user from Admin
         *
         * @param UserAddDto addDto
         * @return string
         **/
        //[Authorize(Roles = "Admin")]
        [HttpPost("adduser")]
        public async Task<ActionResult> AddUser(UserAddDto addDto)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == addDto.Name.ToLower());

            if (existingUser != null)
            {
                if (existingUser.isDeleted == 1)
                {
                    existingUser.UserCode = addDto.Usercode.ToLower();
                    existingUser.Email = addDto.Email.ToLower();
                    existingUser.Name = addDto.Name;
                    existingUser.isDeleted = 0;
                    existingUser.PageNames = addDto.PageNames;
                    existingUser.SelectedWorkCenter = addDto.SelectedWorkCenter;
                    existingUser.Routes = addDto.Routes;
                    existingUser.IsSalesUser = addDto.IsSalesUser;


                    // Remove old roles
                    var userRoles = await _userManager.GetRolesAsync(existingUser);
                    foreach (var role in userRoles)
                    {
                        var removeRoleResult = await _userManager.RemoveFromRoleAsync(existingUser, role);
                        if (!removeRoleResult.Succeeded)
                            return BadRequest(removeRoleResult.Errors);
                    }

                    // Add new role
                    var roleResult = await _userManager.AddToRoleAsync(existingUser, addDto.Role.ToUpper());
                    if (!roleResult.Succeeded)
                        return BadRequest(roleResult.Errors);
                }
                else
                {
                    return BadRequest("Username is already taken, change the UserName.");
                }
            }
            else
            {
                addDto.Usercode = await GenerateUserCode();


                var user = new AppUser
                {
                    UserName = addDto.Name.ToLower(),
                    Name = addDto.Name.ToLower(),
                    UserCode = addDto.Usercode.ToLower(),
                    Email = addDto.Email.ToLower(),
                    IsSalesUser = addDto.IsSalesUser,
                    PageNames = addDto.PageNames,
                    SelectedWorkCenter = addDto.SelectedWorkCenter,
                    Routes = addDto.Routes
                };

                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                // Add role to the newly created user
                var roleResult = await _userManager.AddToRoleAsync(user, addDto.Role.ToUpper());
                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);
            }
            await _context.SaveChangesAsync();

            return Created("User created successfully", "");
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Username is required.");

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username.ToLower());

            if (user == null)
                return NotFound("User does not exist.");

            user.isDeleted = 1; // Mark the user as deleted.

            _context.Entry(user).State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return Ok(new { Message = $"User '{username}' deleted successfully." });

            return StatusCode(500, "Failed to delete user. Please try again.");
        }


        [AllowAnonymous]
        [HttpGet("contact")]
        public async Task<ActionResult> GetContact()
        {
            var contact = await _context.Params.Where(p => p.Name == "contact").Select(p => p.Value).FirstOrDefaultAsync();
            return Ok(contact);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("params")]
        public async Task<ActionResult> UpdateParams(WcDto paramInfo)
        {
            var param = await _context.Params.Where(p => p.Name == paramInfo.tla).SingleOrDefaultAsync();
            if (EqualityComparer<Params>.Default.Equals(param, default(Params)))
            {
                return BadRequest("Could not find param " + paramInfo.tla);
            }

            param.Value = paramInfo.name;
            _context.Entry(param).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var retDesc = paramInfo.tla;
            if (retDesc == "contact")
            {
                retDesc = "Contact Information";
            }
            else if (retDesc == "doc_email")
            {
                retDesc = "Documentation Email Information";
            }

            return Ok(retDesc + " Updated");
        }


        /**
             * Updates the user details
             *
             * @param UserAddDto updateDto
             * @return string
             **/
        //[Authorize(Roles = "Admin")]
        [HttpPut("Ur")]
        public async Task<ActionResult> UpdateUser(UserAddDto updateDto)
        {
            //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .SingleOrDefaultAsync(u => u.UserName == updateDto.username.ToLower());

            user.Name = updateDto.Name;
            user.Email = updateDto.Email;
            user.UserCode = updateDto.Usercode;
            user.PageNames = updateDto.PageNames;
            user.SelectedWorkCenter = updateDto.SelectedWorkCenter;
            user.IsSalesUser = updateDto.IsSalesUser;
            user.Routes = updateDto.Routes;

            //Get the User Roles and then remove them and add the new roles back in
            var roles = user.UserRoles.Select(r => r.Role.Name).ToList();
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, updateDto.Role);

            _context.Entry(user).State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0) return Ok(updateDto.username + " updated sucessfully");

            return BadRequest("Failed to Update User");
        }


    }
}