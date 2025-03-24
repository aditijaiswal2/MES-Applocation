﻿using MES.Server;
using MES.Shared.DTOs;
using MES.Server.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MES.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserDetailsController : ControllerBase
    {
        private readonly ILoginUserDetails _loginUserDetailsService;

        public LoginUserDetailsController(ILoginUserDetails loginUserDetailsService)
        {
            _loginUserDetailsService = loginUserDetailsService;
        }

        [HttpPost("Ua")]
        public async Task<ActionResult<LoginUserDetailsDTO>> CreateLoginUserDetails(LoginUserDetailsDTO loginUserDetailsDto)
        {
            if (loginUserDetailsDto == null)
            {
                return BadRequest("User details are null");
            }

            var createdUserDetails = await _loginUserDetailsService.CreateLoginUserDetailsAsync(loginUserDetailsDto);
            return Ok(createdUserDetails);
        }

        [HttpGet("GD")]
        public  async Task <ActionResult<IEnumerable<LoginUserDetailsDTO>>> GetAllLoginUserDetails()
        {
            var userDetails = await _loginUserDetailsService.GetAllLoginUserDetailsAsync();
            return Ok(userDetails);
        }
    }
    }
