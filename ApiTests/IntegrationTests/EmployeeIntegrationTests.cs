using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = GetEmployees();
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenPaycheckAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}/paycheck");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(1, 2900.81, 346.36, 2554.45)]
    [InlineData(2, 3552.51, 291.33, 3261.18)]
    [InlineData(3, 5508.12, 828.51, 4679.61)]
    public async Task WhenPaycheckAskedForEmployee_ShouldReturnCorrectPaycheck(
        int employeeId,
        decimal expectedGrossPay,
        decimal expectedDeductions,
        decimal expectedNetPay)
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{employeeId}/paycheck");
        var paycheck = new GetPaycheckDto
        {
            Deductions = expectedDeductions,
            Employee = GetEmployees().First(x => x.Id == employeeId),
            GrossPay = expectedGrossPay,
            NetPay = expectedNetPay,
            PayFrequency = PayFrequency.Biweekly
        };
        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }

    private static List<GetEmployeeDto> GetEmployees()
    {
        return new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
    }
}

