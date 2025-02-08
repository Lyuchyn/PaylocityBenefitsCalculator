namespace Api.Services.Tax
{
    public interface ITaxCalculationService
    {
        /// <summary>
        /// Calculates tax rates for a single taxpayer.
        /// </summary>
        /// <remarks>
        /// <see href="https://www.irs.gov/filing/federal-income-tax-rates-and-brackets"/> from more details.
        /// </remarks>
        /// <param name="annualTaxableIncome"></param>
        /// <returns></returns>
        decimal CalculateAnnualFederalTax(decimal annualTaxableIncome);
    }
}
