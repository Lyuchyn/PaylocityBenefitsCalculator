using Api.Configs;
using Api.Models;
using Microsoft.Extensions.Options;

namespace Api.Services.Benefits
{
    public class BenefitService : IBenefitService
    {
        private const int MonthsInYear = 12;

        private readonly BenefitsCostConfig _benefitsCostConfig;

        public BenefitService(IOptions<BenefitsCostConfig> _benefitsCostOptions)
        {
            _benefitsCostConfig = _benefitsCostOptions.Value;
        }

        /// <inheritdoc/>
        public decimal CalculateAnnualBenefitsDeduction(Employee employee)
        {
            var benefitsDeduction =
                GetBaseBenefitsCost() +
                GetAnnualDependentBenefits(employee) +
                GetAnnualDependentOver50YearsBenefits(employee) +
                GetAnnualAdditionalBenefits(employee);

            return benefitsDeduction;
        }

        private decimal GetBaseBenefitsCost()
        {
            return _benefitsCostConfig.BaseBenefitsCostPerMonth * MonthsInYear;
        }

        /// <summary>
        /// Each dependent represents an additional cost per month (for benefits)
        /// </summary>
        private decimal GetAnnualDependentBenefits(Employee employee)
        {
            return employee.Dependents.Count * _benefitsCostConfig.DependentBenefitsCostPerMonth * MonthsInYear;
        }

        /// <summary>
        /// Dependents that are over 50 years old will incur an additional cost per month.
        /// </summary>
        private decimal GetAnnualDependentOver50YearsBenefits(Employee employee)
        {
            return employee.Dependents.Count(x => x.DateOfBirth < DateTime.UtcNow.AddYears(-50))
                * _benefitsCostConfig.DependentOver50YearsBenefitsCostPerMonth * MonthsInYear;
        }

        /// <summary>
        /// Employees that make more than threshold per year will incur an additional percent of their yearly salary in benefits costs.
        /// </summary>
        private decimal GetAnnualAdditionalBenefits(Employee employee)
        {
            if (employee.Salary > _benefitsCostConfig.AdditionalBenefitsSalaryThreshold)
            {
                return employee.Salary * _benefitsCostConfig.AdditionalBenefitsSalaryPercent / 100;
            }
            return 0;
        }
    }
}
