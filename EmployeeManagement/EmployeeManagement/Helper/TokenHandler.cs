using Azure.Core;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Service;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Helper
{
    public interface ITokenHandler
    {
        Task<(string, DateTime)> CreateAccessToken(User user);
        Task<(string, string, DateTime)> CreateRefreshToken(User user);
        Task ValidateToken(TokenValidatedContext context);
        Task<JwtDto> ValidateRefreshToken(string refreshToken);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;

        public TokenHandler(IConfiguration configuration, IUserService userService, IUserTokenService userTokenService)
        {
            _configuration = configuration;
            _userService = userService;
            _userTokenService = userTokenService;
        }

        public async Task<(string, DateTime)> CreateAccessToken(User user)
        {
            DateTime expiredToken = DateTime.Now.AddMinutes(15);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:ValidIssuer"], ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.Integer64, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(15).ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.Name, user.UserName.ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: credential
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            return await Task.FromResult((token, expiredToken));
        }

        public async Task<(string, string, DateTime)> CreateRefreshToken(User user)
        {
            DateTime expiredToken = DateTime.Now.AddHours(3);
            string code = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:ValidIssuer"], ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, ((DateTimeOffset)expiredToken).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.SerialNumber, code, ClaimValueTypes.String, _configuration["JWT:ValidIssuer"])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiredToken,
                signingCredentials: credentials
            );

            string refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return await Task.FromResult((code, refreshToken, expiredToken));
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

        public async Task<JwtDto> ValidateRefreshToken(string refreshToken)
        {
            var claimPriciple = new JwtSecurityTokenHandler().ValidateToken(
                refreshToken,
                new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                },
                out _
                );

            if (claimPriciple == null) return new();

            string code = claimPriciple.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value;

            if (string.IsNullOrEmpty(code)) return new();

            UserToken userToken = await _userTokenService.CheckRefreshToken(code);

            if (userToken != null)
            {
                User user = _userService.GetUserById(userToken.UserId);

                (string newAccessToken, DateTime createdDate) = await CreateAccessToken(user);
                (string codeRefreshToken, string newRefreshToken, DateTime newCreatedDate) = await CreateRefreshToken(user);

                return new JwtDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }

            return new();
        }
    }
}
