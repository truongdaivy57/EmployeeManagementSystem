using AutoMapper;
using Azure.Core;
using EmployeeManagement.Dtos;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repository;
using EmployeeManagement.Services;
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
        Task<IActionResult> SignInAsync(RequestSignInDto dto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SendMail _sendMail;
        private readonly IMapper _mapper;
        private readonly Lazy<ITokenHandler> _tokenHandler;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, SendMail sendMail, IMapper mapper, Lazy<ITokenHandler> tokenHandler, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _sendMail = sendMail;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> SignInAsync(RequestSignInDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new BadRequestObjectResult("User is not exist.");
            }
            if (!user.IsActive || !user.EmailConfirmed)
            {
                return new BadRequestObjectResult("\"You need to confirm your email.\"");
            }

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult("Invalid login attempt.");
            }

            (string accessToken, DateTime expiredDateAccess) = await _tokenHandler.Value.CreateAccessToken(user);
            (string code, string refreshToken, DateTime expiredDateRefresh) = await _tokenHandler.Value.CreateRefreshToken(user);

            //await _userTokenService.SaveToken(new UserToken
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken,
            //    ExpiredDateAccessToken = expiredDateAccess,
            //    ExpiredDateRefreshToken = expiredDateRefresh,
            //    CreatedDate = DateTime.Now,
            //    UserId = user.Id,
            //    IsActive = true,
            //    Key = code
            //});

            return new OkObjectResult(new JwtDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessTokenExpired = expiredDateAccess.ToString("yyyy-MM-dd hh:mm:ss")
            });
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
                if (!await _roleManager.RoleExistsAsync(AppRole.Employee))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(AppRole.Employee));
                }

                await _userManager.AddToRoleAsync(user, AppRole.Employee);
            }

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
    }
}
