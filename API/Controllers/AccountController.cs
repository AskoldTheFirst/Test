using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using API.Biz.Interfaces;
using API.Biz.Service;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;

        private readonly TokenService _tokenService;

        public AccountController(
            ITracer tracer,
            UserManager<User> userManager,
            TokenService tokenService,
            IUnitOfWork uow) : base(uow, tracer)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            await _tracer.TraceAsync("Login", loginDto.Username);

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

        [Authorize]
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

        [Authorize]
        [HttpGet("user-profile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfileAsync()
        {
            string user = User.Identity.Name;
            IUserProfile service = new UserProfileService(_uow);
            return await service.GetUserProfileAsync(user);
        }

        [Authorize]
        [HttpPost("user-profile")]
        public async Task<ActionResult> SaveUserProfileAsync(UserProfileDto profile)
        {
            string user = User.Identity.Name;
            IUserProfile service = new UserProfileService(_uow);
            await service.SaveUserProfileAsync(profile, user);
            return Ok();
        }
    }
}