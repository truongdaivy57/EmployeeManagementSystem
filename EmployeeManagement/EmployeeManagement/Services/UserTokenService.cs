using AutoMapper;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Services
{
    public interface IUserTokenService
    {
        Task SaveToken(UserToken userToken);
        Task<UserToken> CheckRefreshToken(string code);
    }
    public class UserTokenService : IUserTokenService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveToken(UserToken userToken)
        {
            await _unitOfWork.UserTokenRepository.InsertAsync(userToken);
            await _unitOfWork.CommitAsync();
        }

        public async Task<UserToken> CheckRefreshToken(string code)
        {
            return await _unitOfWork.UserTokenRepository.GetSingleByConditionAsync(x => x.Key == code);
        }
    }
}
