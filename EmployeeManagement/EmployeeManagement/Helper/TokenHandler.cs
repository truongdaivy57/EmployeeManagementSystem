using Azure.Core;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Service;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<(string, DateTime)> CreateAccessToken(User user)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expiresAt = issuedAt.AddMinutes(15);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:ValidIssuer"], ClaimValueTypes.String),
                new Claim(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email, null, _configuration["JWT:ValidIssuer"]),
            }
            .Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                NotBefore = issuedAt,
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            string accessToken = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return (accessToken, expiresAt);
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
                new Claim(ClaimTypes.SerialNumber, code, ClaimValueTypes.String, _configuration["JWT:ValidIssuer"]),
                new Claim(ClaimTypes.Email, user.Email),
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

            await _userManager.SetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken", refreshToken);

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

            string email = claimPriciple.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindByEmailAsync(email);

            var token =  await _userManager.GetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken");

            if (!string.IsNullOrEmpty(token))
            {

                (string newAccessToken, DateTime createdDate) = await CreateAccessToken(user);
                (string codeRefreshToken, string newRefreshToken, DateTime newCreatedDate) = await CreateRefreshToken(user);

                return new JwtDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AccessTokenExpired = createdDate.ToString("yyyy-MM-dd hh:mm:ss")
                };
            }

            return new();
        }
    }
}
