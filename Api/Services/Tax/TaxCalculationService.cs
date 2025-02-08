using Api.Configs;
using Microsoft.Extensions.Options;

namespace Api.Services.Tax
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly TaxRateConfig _taxRateConfig;

        public TaxCalculationService(IOptions<TaxRateConfig> taxRateOptions)
        {
            _taxRateConfig = taxRateOptions.Value;
        }

        /// <inheritdoc/>
        public decimal CalculateAnnualFederalTax(decimal annualTaxableIncome)
        {
            decimal taxAmount = 0;
            var taxBrackets = _taxRateConfig.Brackets
                .Where(x => x.IncomeFrom <= annualTaxableIncome)
                .OrderBy(x => x.TaxRatePercent);

            foreach (var bracket in taxBrackets)
            {
                taxAmount += Math.Round((Math.Min(bracket.IncomeTo, annualTaxableIncome) - bracket.IncomeFrom) * bracket.TaxRatePercent / 100, 2);
            }

            return taxAmount;
        }
    }
}
