using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RequestSignUpDto dto)
        {
            var result = await _userService.SignUpAsync(dto);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(RequestSignInDto dto)
        {
            var result = await _userService.SignInAsync(dto);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        //[HttpGet("{userId}")]
        //public async Task<ActionResult<User>> GetUser(int userId)
        //{
        //    var user = await _userService.GetUserById(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<User>>> GetAllUsers()
        //{
        //    return await _userService.GetAllUsers();
        //}

        //[HttpPost]
        //public async Task<ActionResult<User>> AddUser(User user)
        //{
        //    await _userService.AddUser(user);
        //    return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        //}

        //[HttpPut("{userId}")]
        //public async Task<IActionResult> UpdateUser(Guid userId, User user)
        //{
        //    if (userId != user.Id)
        //    {
        //        return BadRequest();
        //    }
        //    await _userService.UpdateUser(user);
        //    return NoContent();
        //}

        //[HttpDelete("{userId}")]
        //public async Task<IActionResult> DeleteUser(int userId)
        //{
        //    await _userService.DeleteUser(userId);
        //    return NoContent();
        //}
    }
}
