using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
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
        User GetUserById(Guid userId);
        IEnumerable<User> GetAllUsers();
        //Task AddUser(User user);
        //Task UpdateUser(User user);
        void DeleteUser(Guid userId);
        Task<IdentityResult> SignUpAsync(RequestSignUpDto dto);
        Task<string> SignInAsync(RequestSignInDto dto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
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

        public User GetUserById(Guid userId)
        {
            return _unitOfWork.UserRepository.Get(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.All();
        }

        //public async Task AddUser(User user)
        //{
        //    await _userRepository.AddUser(user);
        //}

        //public async Task UpdateUser(User user)
        //{
        //    await _userRepository.UpdateUser(user);
        //}

        public void DeleteUser(Guid userId)
        {
            _unitOfWork.UserRepository.Delete(userId);
            _unitOfWork.SaveChanges();
        }
    }
}
