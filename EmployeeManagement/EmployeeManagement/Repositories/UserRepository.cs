using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static EmployeeManagement.Dtos.RequestSignInDto;

namespace EmployeeManagement.Repository
{
    public interface IUserRepository
    {
        //Task<User> GetUserById(int userId);
        //Task<List<User>> GetAllUsers();
        //Task AddUser(User user);
        //Task UpdateUser(User user);
        //Task DeleteUser(int userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<User> GetUserById(int userId)
        //{
        //    return await _context.Users.FindAsync(userId);
        //}

        //public async Task<List<User>> GetAllUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        //public async Task AddUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateUser(User user)
        //{
        //    _context.Entry(user).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteUser(int userId)
        //{
        //    var user = await _context.Users.FindAsync(userId);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
