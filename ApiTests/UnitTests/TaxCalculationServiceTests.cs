using Api.Configs;
using Api.Services.Tax;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ApiTests.UnitTests
{
    /// <summary>
    /// Test class for <see cref="TaxCalculationService"/>.
    /// </summary>
    public class TaxCalculationServiceTests
    {
        private readonly TaxCalculationService _underTest;

        public TaxCalculationServiceTests()
        {
            var taxRateOptionsMock = new Mock<IOptions<TaxRateConfig>>();
            taxRateOptionsMock.SetupGet(x => x.Value)
                .Returns(new TaxRateConfig
                {
                    Brackets = new List<TaxRateBracket>
                    {
                        new()
                        {
                            TaxRatePercent = 10,
                            IncomeFrom = 0,
                            IncomeTo = 11600
                        },
                        new()
                        {
                            TaxRatePercent = 12,
                            IncomeFrom = 11601,
                            IncomeTo = 47150
                        },
                        new()
                        {
                            TaxRatePercent = 22,
                            IncomeFrom = 47151,
                            IncomeTo = 100525
                        },
                        new()
                        {
                            TaxRatePercent = 24,
                            IncomeFrom = 100526,
                            IncomeTo = 191950
                        },
                        new()
                        {
                            TaxRatePercent = 32,
                            IncomeFrom = 191951,
                            IncomeTo = 243725
                        },
                        new()
                        {
                            TaxRatePercent = 35,
                            IncomeFrom = 243726,
                            IncomeTo = 609350
                        }
                    }
                });

            _underTest = new TaxCalculationService(taxRateOptionsMock.Object);
        }

        [Theory]
        [InlineData(11600, 1160)]
        [InlineData(75420, 11645.06)]
        [InlineData(143211, 27412.56)]
        public void CalculateAnnualFederalTax_ShouldReturnCorrectTax(decimal taxableIncome, decimal expectedTax)
        {
            // Act
            var tax = _underTest.CalculateAnnualFederalTax(taxableIncome);

            // Assert
            Assert.Equal(expectedTax, tax);
        }
    }
}
