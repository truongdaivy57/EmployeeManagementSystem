﻿using Azure.Core;
using EmployeeManagement.Dtos;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
using EmployeeManagement.Repository;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SendMail _sendMail;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, SendMail sendMail)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _sendMail = sendMail;
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
            var existUser = _unitOfWork.UserRepository.All().Where(x => x.Email == dto.Email).FirstOrDefault();
            if (existUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email đã được sử dụng." });
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
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var request = new HttpContextAccessor().HttpContext.Request;
                var confirmationLink = await ConfirmEmail(token, user.Email);
                _sendMail.SendEmail(user.Email, "Confirm your account", confirmationLink);
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Đăng ký không thành công." });
        }

        private async Task<string> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                var confirmationLink = $"{baseUrl}/api/user/ConfirmEmail?token={token}&email={user.Email}";
                return confirmationLink;
            }

            return null;
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
