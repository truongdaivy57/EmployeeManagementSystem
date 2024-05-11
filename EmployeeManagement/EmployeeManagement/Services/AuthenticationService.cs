using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IAuthenticationService
    {

    }

    public class AuthenticationService
    {
        private readonly string Key = "suifbweudfwqudgweufgewufgwefcgweiudgweidgwed";

        public AuthenticationService() { }

        //public ResponseLoginDto Authenticator(RequestLoginDto requestLoginDto)
        //{
        //    //var account =             //viết repository get UserEntity
        //    //if (account == null)
        //    //{
        //    // kiểm tra nếu user == null thì thorw ex
        //    //}
        //    var account = new User();

        //    var token = CreateJwtToken(account);
        //    var refreshToken = CreateRefreshToken(account);
        //    var result = new ResponseLoginModel
        //    {
        //        FullName = account.FirstName,
        //        UserId = account.Id,
        //        Token = token,
        //        RefreshToken = refreshToken.Token
        //    };
        //    return result;
        //}

        //private RefreshToken CreateRefreshToken(User user)
        //{
        //    var randomByte = new byte[64];
        //    var token = ""; // Viết hàm tạo chuỗi random string
        //    var refreshToken = new RefreshTokens
        //    {
        //        UserId = account.Id,
        //        Expires = DateTime.Now.AddDays(1),
        //        IsActive = true,
        //        Token = token
        //    };
        //    // viết code insert refreshToken vào DB
        //    return refreshToken;
        //}
    }
}
