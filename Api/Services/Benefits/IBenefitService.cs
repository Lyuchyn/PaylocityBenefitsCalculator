using Api.Models;

namespace Api.Services.Benefits
{
    public interface IBenefitService
    {
        /// <summary>
        /// Calculates annual benefits deduction amount for the provided employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        decimal CalculateAnnualBenefits(Employee employee);
    }
}
