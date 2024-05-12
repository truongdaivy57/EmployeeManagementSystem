using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services
{
    public interface IAuthenticationService
    {

    }

    public class AuthenticationService
    {
        private readonly string Key = "suifbweudfwqudgweufgewufgwefcgweiudgweidgwed";

        public AuthenticationService() { }

        public ResponseSignInDto Authenticator(RequestSignInDto requestSignInDto)
        {
            //var account =             //viết repository get UserEntity
            //if (account == null)
            //{
            // kiểm tra nếu user == null thì thorw ex
            //}
            var user = new User();

            var token = CreateJwtToken(user);
            var refreshToken = CreateRefreshToken(user);
            var result = new ResponseSignInDto
            {
                FullName = user.FirstName,
                Token = token,
                RefreshToken = refreshToken.Token
            };
            return result;
        }

        private RefreshToken CreateRefreshToken(User user)
        {
            var randomByte = new byte[64];
            var token = ""; // Viết hàm tạo chuỗi random string
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Expire = DateTime.Now.AddDays(1),
                IsActive = true,
                Token = token
            };
            // viết code insert refreshToken vào DB
            return refreshToken;
        }

        private string CreateJwtToken(User user)
        {
            var tokenHanler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Key);
            var securityKey = new SymmetricSecurityKey(key);
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Email, "sdasdasd"),
                    new Claim("CarNumber", "1")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credential
            };
            var token = tokenHanler.CreateToken(tokenDescription);
            return tokenHanler.WriteToken(token);
        }
    }
}
