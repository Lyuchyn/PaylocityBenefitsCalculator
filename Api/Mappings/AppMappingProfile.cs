using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mappings
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Employee, GetEmployeeDto>(MemberList.Destination);
            CreateMap<Dependent, GetDependentDto>(MemberList.Destination);
        }
    }
}
