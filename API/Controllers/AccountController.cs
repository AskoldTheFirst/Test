using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using API.Database;
using API.DTOs;
using API.Database.Entities;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;

        private readonly TestDbContext _ctx;

        private readonly UserManager<User> _userManager;

        public AccountController(TestDbContext context, ILogger<AccountController> logger, UserManager<User> userManager)
        {
            _ctx = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201);
        }
    }
}