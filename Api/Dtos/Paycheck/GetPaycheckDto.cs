using Api.Dtos.Employee;
using Api.Models;

namespace Api.Dtos.Paycheck
{
    public class GetPaycheckDto
    {
        public GetEmployeeDto Employee { get; set; } = null!;

        /// <summary>
        /// Pay frequency (biweekly, weekly, monthly)
        /// </summary>
        public PayFrequency PayFrequency { get; set; }

        /// <summary>
        /// Total earnings before deductions.
        /// </summary>
        public decimal GrossPay { get; set; }

        /// <summary>
        /// Taxes, insurance etc.
        /// </summary>
        public decimal Deductions { get; set; }

        /// <summary>
        /// The final amount the employee takes home after deductions.
        /// </summary>
        public decimal NetPay { get; set; }
    }
}
