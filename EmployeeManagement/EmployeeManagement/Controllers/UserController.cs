using AutoMapper;
using EmployeeManagement.Dtos;
using EmployeeManagement.Helper;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;

        public UserController(IUserService userService, IMapper mapper, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
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

            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]/refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto token)
        {
            return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
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

        [HttpPost]
        [Route("api/[controller]/forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return await _userService.ForgotPassword(email);
        }

        [HttpPut]
        [Route("api/[controller]/reset-password")]
        public async Task<IActionResult> RestPassword(string email, string otp, string newPassword)
        {
            return await _userService.ResetPassword(email, otp, newPassword);
        }

        [HttpPost]
        [Route("api/[controller]/sign-out")]
        public async Task<IActionResult> SignOut()
        {
            return await _userService.SignOut();
        }
    }
}
