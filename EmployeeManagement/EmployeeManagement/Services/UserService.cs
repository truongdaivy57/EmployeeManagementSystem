using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Service
{
    public interface IUserService
    {
        //Task<User> GetUserById(int userId);
        //Task<List<User>> GetAllUsers();
        //Task AddUser(User user);
        //Task UpdateUser(User user);
        //Task DeleteUser(int userId);
        Task<IdentityResult> SignUpAsync(RequestSignUpDto dto);
        Task<string> SignInAsync(RequestSignInDto dto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> SignInAsync(RequestSignInDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(RequestSignUpDto dto)
        {
            var user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email
            };

            return await _userManager.CreateAsync(user, dto.Password);
        }

        //public async Task<User> GetUserById(int userId)
        //{
        //    return await _userRepository.GetUserById(userId);
        //}

        //public async Task<List<User>> GetAllUsers()
        //{
        //    return await _userRepository.GetAllUsers();
        //}

        //public async Task AddUser(User user)
        //{
        //    await _userRepository.AddUser(user);
        //}

        //public async Task UpdateUser(User user)
        //{
        //    await _userRepository.UpdateUser(user);
        //}

        //public async Task DeleteUser(int userId)
        //{
        //    await _userRepository.DeleteUser(userId);
        //}
    }
}
