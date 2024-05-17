using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class UserClaimController : ControllerBase
    {
        private readonly IUserClaimService _userClaimService;
        private readonly UserManager<User> _userManager;

        public UserClaimController(IUserClaimService userClaimService, UserManager<User> userManager)
        {
            _userClaimService = userClaimService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/[controller]/get-all-luser-claims")]
        public async Task<IActionResult> GetAllUserClaims()
        {
            var users = _userManager.Users.ToList();
            var allClaims = new List<UserClaimsDto>();

            foreach (var user in users)
            {
                var claims = await _userClaimService.GetUserClaimsAsync(user);
                allClaims.Add(new UserClaimsDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Claims = claims.Select(c => new ClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList()
                });
            }

            return Ok(allClaims);
        }


        [HttpGet]
        [Route("api/[controller]/get-user-claims")]
        public async Task<IActionResult> GetUserClaims(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound();

            var claims = await _userClaimService.GetUserClaimsAsync(user);
            return Ok(claims);
        }

        [HttpPost]
        [Route("api/[controller]/add-user-claim")]
        public async Task<IActionResult> AddUserClaim(Guid userId, [FromBody] ClaimDto claimDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound();

            var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);
            await _userClaimService.AddUserClaimAsync(user, claim);
            return Ok();
        }

        [HttpDelete]
        [Route("api/[controller]/delete-user-claims")]
        public async Task<IActionResult> RemoveUserClaim(Guid userId, [FromBody] ClaimDto claimDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound();

            var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);
            await _userClaimService.RemoveUserClaimAsync(user, claim);
            return Ok();
        }

        [HttpPut]
        [Route("api/[controller]/update-user-claim")]
        public async Task<IActionResult> UpdateUserClaim(Guid userId, [FromBody] UpdateClaimDto updateClaimDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound();

            var oldClaim = new Claim(updateClaimDto.OldClaimType, updateClaimDto.OldClaimValue);
            var newClaim = new Claim(updateClaimDto.NewClaimType, updateClaimDto.NewClaimValue);
            await _userClaimService.UpdateUserClaimAsync(user, oldClaim, newClaim);
            return Ok();
        }
    }
}
