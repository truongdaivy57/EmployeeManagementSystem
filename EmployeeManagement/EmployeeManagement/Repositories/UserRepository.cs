using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static EmployeeManagement.Dtos.RequestSignInDto;

namespace EmployeeManagement.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public override User Add(User user)
        {
            return base.Add(user);
        }

        public override User Get(Guid id)
        {
            return base.Get(id);
        }

        public override IEnumerable<User> All()
        {
            return base.All();
        }

        public override User Update(User user)
        {
            return base.Update(user);
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
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
