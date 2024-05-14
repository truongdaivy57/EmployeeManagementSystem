using AutoMapper;
using EmployeeManagement.Dtos;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/[controller]/sign-up")]
        public async Task<IActionResult> SignUp(RequestSignUpDto dto)
        {
            var result = await _userService.SignUpAsync(dto);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("api/[controller]/confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string otp)
        {
            return await _userService.ConfirmEmail(email, otp);
        }

        [HttpPost]
        [Route("api/[controller]/sign-in")]
        public async Task<IActionResult> SignIn(RequestSignInDto dto)
        {
            var result = await _userService.SignInAsync(dto);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/[controller]/get-user")]
        public IActionResult GetUser(Guid userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("api/[controller]/get-all-users")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPut]
        [Route("api/[controller]/update-user")]
        public IActionResult UpdateUser(Guid userId, UserDto dto)
        {
            var existUser = _userService.GetUserById(userId);
            if (existUser == null)
            {
                return BadRequest();
            }

            _mapper.Map(dto, existUser);

            return Ok(_userService.UpdateUser(existUser));
        }

        [HttpDelete]
        [Route("api/[controller]/delete-user")]
        public IActionResult DeleteUser(Guid userId)
        {
            _userService.DeleteUser(userId);
            return Ok(_userService.GetAllUsers());
        }
    }
}
