using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Employees;
using Api.Services.Paycheck;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IPaycheckService _paycheckService;

    public EmployeesController(IEmployeeService employeeService, IPaycheckService paycheckService)
    {
        _employeeService = employeeService;
        _paycheckService = paycheckService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeService.GetById(id);

        return employee is null
            ? NotFound() 
            : new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true
            };
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var employees = await _employeeService.GetAll();

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Gets the employee's paycheck")]
    [HttpGet("{id}/paycheck")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> GetEmployeePaycheck(int id)
    {
        var paycheck = await _paycheckService.GetPaycheck(id);

        return paycheck is null
            ? NotFound($"Employee not found for Id={id}")
            : new ApiResponse<GetPaycheckDto>
            {
                Data = paycheck,
                Success = true
            };
    }
}
