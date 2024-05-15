using AutoMapper;
using EmployeeManagement.Dtos;
using EmployeeManagement.Model;

namespace EmployeeManagement.Helper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<JwtDto, User>().ReverseMap();
        }
    }
}
