using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using API.Database;
using API.DTOs;
using API.Database.Entities;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;

        private readonly TestDbContext _ctx;

        private readonly UserManager<User> _userManager;

        private readonly TokenService _tokenService;

        public AccountController(TestDbContext context, ILogger<AccountController> logger, UserManager<User> userManager, TokenService tokenService)
        {
            _ctx = context;
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user),
                Login = user.UserName
            };
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

        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateTokenAsync(user),
                Login = user.UserName
            };
        }
    }
}