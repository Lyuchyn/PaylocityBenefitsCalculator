using System.ComponentModel.DataAnnotations;

namespace Api.Configs
{
    public class BenefitsCostConfig
    {
        public const string SectionName = "BenefitsCostConfig";

        /// <summary>
        /// Employees have a base benefits cost per month.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal BaseBenefitsCostPerMonth { get; set; }

        /// <summary>
        /// Each dependent represents an additional benefits cost per month.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal DependentBenefitsCostPerMonth { get; set; }

        /// <summary>
        /// Dependents that are over 50 years old will incur an additional <see cref="DependentOver50YearsBenefitsCostPerMonth"> per month.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal DependentOver50YearsBenefitsCostPerMonth { get; set; }

        /// <summary>
        /// Employees that make more than <see cref="AdditionalBenefitsSalaryThreshold"> per year
        /// will incur an additional <see cref="AdditionalBenefitsSalaryPercent"> of their yearly salary in benefits costs.
        /// </summary>
        [Range(0, double.MaxValue)]
        public decimal AdditionalBenefitsSalaryThreshold { get; set; }

        [Range(0, 100)]
        public decimal AdditionalBenefitsSalaryPercent { get; set; }
    }
}
