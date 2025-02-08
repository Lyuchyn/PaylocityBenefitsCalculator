using Api.Dtos.Employee;

namespace Api.Services.Employees
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>List of employees or empty list.</returns>
        Task<List<GetEmployeeDto>> GetAll();

        /// <summary>
        /// Gets an employee by id.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <returns>The employee found, or <see langword="null" />.</returns>
        Task<GetEmployeeDto?> GetById(int id);
    }
}
