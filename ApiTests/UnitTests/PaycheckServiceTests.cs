using Api.Configs;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repositories;
using Api.Services.Benefits;
using Api.Services.Paycheck;
using Api.Services.Tax;
using ApiTests.Fixtures;
using AutoBogus;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    /// <summary>
    /// Test class for <see cref="PaycheckService"/>.
    /// </summary>
    public class PaycheckServiceTests : IClassFixture<AutoMapperFixture>
    {
        private readonly PaycheckService _underTest;

        private readonly Mock<IEmployeesRepository> _employeesRepositoryMock = new();
        private readonly Mock<IOptions<PaycheckConfig>> _paycheckOptionsMock = new();
        private readonly Mock<IBenefitService> _benefitServiceMock = new();
        private readonly Mock<ITaxCalculationService> _taxCalculationServiceMock = new();

        public PaycheckServiceTests(AutoMapperFixture autoMapperFixture)
        {
            _paycheckOptionsMock.SetupGet(x => x.Value)
                .Returns(new PaycheckConfig { PayFrequency = PayFrequency.Biweekly });

            _underTest = new PaycheckService(
                _employeesRepositoryMock.Object,
                autoMapperFixture.Mapper,
                _paycheckOptionsMock.Object,
                _benefitServiceMock.Object,
                _taxCalculationServiceMock.Object);
        }

        [Fact]
        public async Task GetPaycheck_ShouldReturnNull_WhenEmployeeNotFound()
        {
            // Arrange
            var employeeId = 100;
            _employeesRepositoryMock.Setup(x => x.GetById(employeeId)).ReturnsAsync((Employee?)null);

            // Act
            var result = await _underTest.GetPaycheck(employeeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPaycheck_ShouldGetBiweeklyPaycheck()
        {
            // Arrange
            var employee = new AutoFaker<Employee>().RuleFor(x => x.Salary, 92365).Generate();
            var employeeId = 1;
            var annualBenefitsDeduction = 13847.30m;
            var annualTaxableIncome = 78517.7m;
            var expectedTax = 12733m;

            _employeesRepositoryMock.Setup(x => x.GetById(employeeId))
                .ReturnsAsync(employee);

            _benefitServiceMock.Setup(x => x.CalculateAnnualBenefits(employee))
                .Returns(annualBenefitsDeduction);

            _taxCalculationServiceMock
                .Setup(x => x.CalculateAnnualFederalTax(annualTaxableIncome))
                .Returns(expectedTax);

            // Act
            var result = await _underTest.GetPaycheck(employeeId);

            // Assert
            Assert.IsType<GetPaycheckDto>(result);
            Assert.NotNull(result);
            Assert.IsType<GetEmployeeDto>(result!.Employee);
            Assert.NotNull(result.Employee);
            Assert.Equal(employee.Id, result.Employee.Id);
            Assert.Equal(PayFrequency.Biweekly, result.PayFrequency);
            Assert.Equal(3552.5m, result.GrossPay);
            Assert.Equal(489.73m, result.Deductions);
            Assert.Equal(3062.77m, result.NetPay);
        }
    }
}
