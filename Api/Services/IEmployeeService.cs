using Api.Dtos.Employee;

namespace Api.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List of employees or empty list.</returns>
        Task<List<GetEmployeeDto>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Gets an employee by id.
        /// </summary>
        /// <param name="id">Employee id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The employee found, or <see langword="null" />.</returns>
        Task<GetEmployeeDto?> GetById(int id, CancellationToken cancellationToken);
    }
}
