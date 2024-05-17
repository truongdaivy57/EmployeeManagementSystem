using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public interface IRoleClaimService
    {
        Task<IEnumerable<Claim>> GetRoleClaimsAsync(IdentityRole<Guid> role);
        Task AddRoleClaimAsync(IdentityRole<Guid> role, Claim claim);
        Task RemoveRoleClaimAsync(IdentityRole<Guid> role, Claim claim);
        Task UpdateRoleClaimAsync(IdentityRole<Guid> role, Claim oldClaim, Claim newClaim);
    }

    public class RoleClaimService : IRoleClaimService
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleClaimService(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<Claim>> GetRoleClaimsAsync(IdentityRole<Guid> role)
        {
            var claims = await _roleManager.GetClaimsAsync(role);
            return claims;
        }

        public async Task AddRoleClaimAsync(IdentityRole<Guid> role, Claim claim)
        {
            await _roleManager.AddClaimAsync(role, claim);
        }

        public async Task RemoveRoleClaimAsync(IdentityRole<Guid> role, Claim claim)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        public async Task UpdateRoleClaimAsync(IdentityRole<Guid> role, Claim oldClaim, Claim newClaim)
        {
            await _roleManager.RemoveClaimAsync(role, oldClaim);
            await _roleManager.AddClaimAsync(role, newClaim);
        }
    }

}
