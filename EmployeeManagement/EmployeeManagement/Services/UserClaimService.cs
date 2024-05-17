using EmployeeManagement.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EmployeeManagement.Services
{
    public interface IUserClaimService
    {
        Task<IEnumerable<Claim>> GetUserClaimsAsync(User user);
        Task AddUserClaimAsync(User user, Claim claim);
        Task RemoveUserClaimAsync(User user, Claim claim);
        Task UpdateUserClaimAsync(User user, Claim oldClaim, Claim newClaim);
    }

    public class UserClaimService : IUserClaimService
    {
        private readonly UserManager<User> _userManager;

        public UserClaimService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims;
        }

        public async Task AddUserClaimAsync(User user, Claim claim)
        {
            await _userManager.AddClaimAsync(user, claim);
        }

        public async Task RemoveUserClaimAsync(User user, Claim claim)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        public async Task UpdateUserClaimAsync(User user, Claim oldClaim, Claim newClaim)
        {
            await _userManager.RemoveClaimAsync(user, oldClaim);
            await _userManager.AddClaimAsync(user, newClaim);
        }
    }

}
