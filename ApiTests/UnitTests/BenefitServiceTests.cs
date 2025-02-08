using Api.Configs;
using Api.Models;
using Api.Services.Benefits;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace ApiTests.UnitTests
{
    /// <summary>
    /// Test class for <see cref="BenefitService"/>.
    /// </summary>
    public class BenefitServiceTests
    {
        private readonly BenefitService _underTest;

        public BenefitServiceTests()
        {
            var benefitsCostOptionsMock = new Mock<IOptions<BenefitsCostConfig>>();
            benefitsCostOptionsMock.SetupGet(x => x.Value)
                .Returns(new BenefitsCostConfig
                {
                    AdditionalBenefitsSalaryPercent = 2,
                    AdditionalBenefitsSalaryThreshold = 80000,
                    BaseBenefitsCostPerMonth = 1000,
                    DependentBenefitsCostPerMonth = 600,
                    DependentOver50YearsBenefitsCostPerMonth = 200
                });

            _underTest = new BenefitService(benefitsCostOptionsMock.Object);
        }

        [Fact]
        public void CalculateAnnualBenefitsDeduction_ShouldReturnBaseBenefitsCostOnly()
        {
            // Arrange
            var employee = new Employee
            {
                DateOfBirth = new DateTime(1988, 1, 1),
                Salary = 50000
            };

            // Act
            var result = _underTest.CalculateAnnualBenefitsDeduction(employee);

            // Assert
            Assert.Equal(12000, result);
        }

        [Fact]
        public void CalculateAnnualBenefitsDeduction_ShouldIncludeDependentBenefits()
        {
            // Arrange
            var child1 = new Dependent 
            {
                DateOfBirth = new DateTime(2018, 1, 1),
                Relationship = Relationship.Child
            };
            var child2 = new Dependent 
            {
                DateOfBirth = new DateTime(2022, 1, 1),
                Relationship = Relationship.Child
            };
            var spouse = new Dependent
            {
                DateOfBirth = new DateTime(1970, 1, 1),
                Relationship = Relationship.Spouse
            };
            var employee = new Employee
            {
                DateOfBirth = new DateTime(1968, 1, 1),
                Salary = 50000,
                Dependents = { child1, child2, spouse }
            };

            // Act
            var result = _underTest.CalculateAnnualBenefitsDeduction(employee);

            // Assert
            Assert.Equal(36000, result);
        }

        [Fact]
        public void CalculateAnnualBenefitsDeduction_ShouldIncludeAdditionalBenefitsForSalaryExceedingThreshold()
        {
            // Arrange
            var employee = new Employee
            {
                DateOfBirth = new DateTime(1988, 1, 1),
                Salary = 92365
            };

            // Act
            var result = _underTest.CalculateAnnualBenefitsDeduction(employee);

            // Assert
            Assert.Equal(13847.30m, result);
        }
    }
}
