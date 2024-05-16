using AutoMapper;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;
using EmployeeManagement.Service;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/[controller]/add-department")]
        public IActionResult AddDepartment(DepartmentDto dto)
        {
            var department = new Department();
            _mapper.Map(dto, department);
            return Ok(_departmentService.AddDepartment(department));
        }

        [HttpGet]
        [Route("api/[controller]/get-department")]
        public IActionResult GetDepartment(Guid departmentId)
        {
            var department = _departmentService.GetDepartmentById(departmentId);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet]
        [Route("api/[controller]/get-all-departments")]
        public IActionResult GetAllDepartments()
        {
            return Ok(_departmentService.GetAllDepartments());
        }

        [HttpPut]
        [Route("api/[controller]/update-department")]
        public IActionResult UpdateDepartment(Guid departmentId, DepartmentDto dto)
        {
            var existDepartment = _departmentService.GetDepartmentById(departmentId);
            if (existDepartment == null)
            {
                return BadRequest();
            }

            _mapper.Map(dto, existDepartment);

            return Ok(_departmentService.UpdateDepartment(existDepartment));
        }

        [HttpDelete]
        [Route("api/[controller]/delete-department")]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            _departmentService.DeleteDepartment(departmentId);
            return Ok(_departmentService.GetAllDepartments());
        }

        [HttpPut]
        [Route("api/[controller]/assign-department")]
        public IActionResult AssignDepartment(Guid departmentId, Guid userId)
        {
            _departmentService.AssignDepartment(departmentId, userId);
            return Ok(_departmentService.GetDepartmentById(departmentId));
        }
    }
}
