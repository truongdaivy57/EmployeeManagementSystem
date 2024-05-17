using EmployeeManagement.Dtos;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class RoleClaimController : ControllerBase
    {
        private readonly IRoleClaimService _roleClaimService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleClaimController(IRoleClaimService roleClaimService, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleClaimService = roleClaimService;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("api/[controller]/get-all-role-claims")]
        public async Task<IActionResult> GetAllRoleClaims()
        {
            var roles = _roleManager.Roles.ToList();
            var allClaims = new List<RoleClaimsDto>();

            foreach (var role in roles)
            {
                var claims = await _roleClaimService.GetRoleClaimsAsync(role);
                allClaims.Add(new RoleClaimsDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Claims = claims.Select(c => new ClaimDto { ClaimType = c.Type, ClaimValue = c.Value }).ToList()
                });
            }

            return Ok(allClaims);
        }

        [HttpGet]
        [Route("api/[controller]/get-role-claims")]
        public async Task<IActionResult> GetRoleClaims(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return NotFound();

            var claims = await _roleClaimService.GetRoleClaimsAsync(role);
            return Ok(claims);
        }

        [HttpPost]
        [Route("api/[controller]/add-role-claim")]
        public async Task<IActionResult> AddRoleClaim(Guid roleId, [FromBody] ClaimDto claimDto)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return NotFound();

            var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);
            await _roleClaimService.AddRoleClaimAsync(role, claim);
            return Ok();
        }

        [HttpDelete]
        [Route("api/[controller]/delete-role-claim")]
        public async Task<IActionResult> RemoveRoleClaim(Guid roleId, [FromBody] ClaimDto claimDto)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return NotFound();

            var claim = new Claim(claimDto.ClaimType, claimDto.ClaimValue);
            await _roleClaimService.RemoveRoleClaimAsync(role, claim);
            return Ok();
        }

        [HttpPut]
        [Route("api/[controller]/update-role-claim")]
        public async Task<IActionResult> UpdateRoleClaim(Guid roleId, [FromBody] UpdateClaimDto updateClaimDto)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return NotFound();

            var oldClaim = new Claim(updateClaimDto.OldClaimType, updateClaimDto.OldClaimValue);
            var newClaim = new Claim(updateClaimDto.NewClaimType, updateClaimDto.NewClaimValue);
            await _roleClaimService.UpdateRoleClaimAsync(role, oldClaim, newClaim);
            return Ok();
        }
    }
}
