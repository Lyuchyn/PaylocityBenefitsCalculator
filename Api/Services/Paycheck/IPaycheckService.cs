using Api.Dtos.Paycheck;

namespace Api.Services.Paycheck
{
    public interface IPaycheckService
    {
        /// <summary>
        /// Gets the employee's paycheck.
        /// </summary>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>The paycheck or <see langword="null" /> if employee not found.</returns>
        Task<GetPaycheckDto?> GetPaycheck(int employeeId);
    }
}
