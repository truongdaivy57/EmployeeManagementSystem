using Azure.Core;
using EmployeeManagement.Dtos;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
        Task<IActionResult> ConfirmEmail(string email, string otp);
        User UpdateUser(User user);
        void DeleteUser(Guid userId);
        Task<IdentityResult> SignUpAsync(RequestSignUpDto dto);
        Task<string> SignInAsync(RequestSignInDto dto);
        Task ValidateToken(TokenValidatedContext tokenValidatedContext);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SendMail _sendMail;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IUnitOfWork unitOfWork, SendMail sendMail)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _sendMail = sendMail;
        }

        public async Task<string> SignInAsync(RequestSignInDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return "Invalid login attempt.";
            }
            if (!user.IsActive || !user.EmailConfirmed)
            {
                return "You need to confirm your email.";
            }
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            if (!result.Succeeded)
            {
                return "Invalid login attempt.";
            }

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:ValidIssuer"], ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.Integer64, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddHours(3).ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.Name, user.UserName.ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.Email, dto.Email),
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(RequestSignUpDto dto)
        {
            var existUser = _unitOfWork.UserRepository.All().Where(x => x.Email == dto.Email).FirstOrDefault();
            if (existUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email đã được sử dụng." });
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Mật khẩu xác nhận không đúng." });
            }

            var user = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email,
                IsActive = false,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                string otp = GenerateOtp();
                user.VerificationToken = otp;
                await _userManager.UpdateAsync(user);
                _sendMail.SendEmail(user.Email, "Confirm your account", $"Your OTP code is: {otp}");
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Đăng ký không thành công." });
        }

        public async Task<IActionResult> ConfirmEmail(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new NotFoundResult();

            if (user.VerificationToken != otp)
            {
                return new BadRequestObjectResult("Invalid OTP.");
            }

            user.EmailConfirmed = true;
            user.IsActive = true;
            user.VerificationToken = null;
            await _userManager.UpdateAsync(user);

            return new OkObjectResult("Email confirmed successfully.");
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 1000000);
            return otp.ToString();
        }

        public User GetUserById(Guid userId)
        {
            return _unitOfWork.UserRepository.Get(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepository.All();
        }

        public User UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChanges();
            return user;
        }

        public void DeleteUser(Guid userId)
        {
            _unitOfWork.UserRepository.Delete(userId);
            _unitOfWork.SaveChanges();
        }

        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();

            if (claims.Count == 0)
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal.Identity as ClaimsIdentity;

            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                context.Fail("This token is not issued by point entry");
                return;
            }

            if (identity.FindFirst(JwtRegisteredClaimNames.Exp) == null)
            {
                var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp).Value;
                long ticks = long.Parse(dateExp);
                var date = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;
                var minutes = date.Subtract(DateTime.Now).TotalMinutes;

                if (minutes < 0)
                {
                    context.Fail("This token is expired.");
                    throw new Exception("This token is expired.");
                    return;
                }
            }

            //record log, update last date
        }
    }
}
