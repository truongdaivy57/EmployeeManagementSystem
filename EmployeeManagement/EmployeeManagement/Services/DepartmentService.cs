using EmployeeManagement.Dtos;
using EmployeeManagement.Helper;
using EmployeeManagement.Model;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services
{
    public interface IDepartmentService
    {
        Department AddDepartment(Department department);
        Department GetDepartmentById(Guid departmentId);
        IEnumerable<Department> GetAllDepartments();
        Department UpdateDepartment(Department department);
        void DeleteDepartment(Guid departmentId);
        void AssignDepartment(Guid departmentId, Guid userId);
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Department GetDepartmentById(Guid departmentId)
        {
            return _unitOfWork.DepartmentRepository.Get(departmentId);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _unitOfWork.DepartmentRepository.GetAllIncluding(d => d.Users);
        }

        public Department UpdateDepartment(Department department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.SaveChanges();
            return department;
        }

        public void DeleteDepartment(Guid departmentId)
        {
            var department = _unitOfWork.DepartmentRepository
                .GetAllIncluding(d => d.Users)
                .FirstOrDefault(d => d.Id == departmentId);
            if (department.Users != null)
            {
                throw new Exception("Department contains user(s).");
            }
            _unitOfWork.DepartmentRepository.Delete(departmentId);
            _unitOfWork.SaveChanges();
        }

        public Department AddDepartment(Department department)
        {
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.SaveChanges();
            return department;
        }

        public void AssignDepartment(Guid departmentId, Guid userId)
        {
            var department = _unitOfWork.DepartmentRepository
                .GetAllIncluding(d => d.Users)
                .FirstOrDefault(d => d.Id == departmentId);

            if (department == null)
            {
                throw new Exception("Department not found");
            }

            var user = _unitOfWork.UserRepository.Get(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (department.Users != null && department.Users.Any(u => u.Id == userId))
            {
                throw new Exception("User is already assigned to this department");
            }

            if (user.DepartmentId != null && user.DepartmentId != departmentId)
            {
                var oldDepartment = _unitOfWork.DepartmentRepository
                .GetAllIncluding(d => d.Users)
                .FirstOrDefault(d => d.Id == user.DepartmentId);
                oldDepartment.Users.Remove(user);
            }

            user.DepartmentId = departmentId;

            if (department.Users == null)
            {
                department.Users = new List<User>();
            }

            department.Users.Add(user);

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.DepartmentRepository.Update(department);

            // Save changes
            _unitOfWork.SaveChanges();
        }

    }
}
