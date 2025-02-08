using Api.Dtos.Employee;
using Api.Repositories;
using AutoMapper;

namespace Api.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeesRepository employeesRepository, IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<List<GetEmployeeDto>> GetAll()
        {
            var employees = await _employeesRepository.GetAll();
            return _mapper.Map<List<GetEmployeeDto>>(employees);
        }

        /// <inheritdoc/>
        public async Task<GetEmployeeDto?> GetById(int id)
        {
            var employee = await _employeesRepository.GetById(id);
            return _mapper.Map<GetEmployeeDto>(employee);
        }
    }
}
