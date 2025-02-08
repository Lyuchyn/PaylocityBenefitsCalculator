using Api.Configs;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Repositories;
using Api.Services.Benefits;
using Api.Services.Tax;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Api.Services.Paycheck
{
    public class PaycheckService : IPaycheckService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMapper _mapper;
        private readonly PaycheckConfig _paycheckConfig;
        private readonly IBenefitService _benefitService;
        private readonly ITaxCalculationService _taxCalculationService;

        public PaycheckService(
            IEmployeesRepository employeesRepository,
            IMapper mapper,
            IOptions<PaycheckConfig> paycheckOptions,
            IBenefitService benefitService,
            ITaxCalculationService taxCalculationService)
        {
            _employeesRepository = employeesRepository;
            _mapper = mapper;
            _paycheckConfig = paycheckOptions.Value;
            _benefitService = benefitService;
            _taxCalculationService = taxCalculationService;
        }

        /// <inheritdoc/>
        public async Task<GetPaycheckDto?> GetPaycheck(int employeeId)
        {
            /*
             A paycheck typically includes:
             - Gross pay (total earnings before deductions)
             - Deductions (such as taxes, insurance, retirement contributions)
             - Net pay (the final amount the employee takes home after deductions)
             This code calculates the paycheck by following formula (for simplicity we calculate only federal tax):
                NetPay = GrossPay + Benefits − FederalTaxDeduction
             */

            var employee = await _employeesRepository.GetById(employeeId);
            if (employee is null)
            {
                return null;
            }

            decimal benefits = _benefitService.CalculateAnnualBenefits(employee);
            decimal annualTaxableIncome = employee.Salary - benefits;
            decimal taxDeduction = _taxCalculationService.CalculateAnnualFederalTax(annualTaxableIncome);
            decimal netPay = GetAmountPerPaycheck(employee.Salary - taxDeduction);

            return new GetPaycheckDto
            {
                Employee = _mapper.Map<GetEmployeeDto>(employee),
                PayFrequency = _paycheckConfig.PayFrequency,
                GrossPay = GetAmountPerPaycheck(employee.Salary),
                Deductions = GetAmountPerPaycheck(taxDeduction),
                NetPay = netPay
            };
        }

        private static int GetPaychecksCountPerYear(PayFrequency payFrequency) => payFrequency switch
        {
            PayFrequency.Biweekly => 26,
            PayFrequency.Weekly => 52,
            PayFrequency.Monthly => 12,
            _ => throw new ArgumentOutOfRangeException(nameof(payFrequency))
        };

        private decimal GetAmountPerPaycheck(decimal amount)
        {
            int paychecksPerYear = GetPaychecksCountPerYear(_paycheckConfig.PayFrequency);
            return Math.Round(amount / paychecksPerYear, 2);
        }
    }
}
