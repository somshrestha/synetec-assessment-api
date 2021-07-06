using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService :IBonusPoolService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMappingService _mappingService;
        private readonly ICalculationService _calculationService;

        public BonusPoolService(IMappingService mappingService,
                                IEmployeeService employeeService,
                                ICalculationService calculationService)
        {
            _mappingService = mappingService;
            _employeeService = employeeService;
            _calculationService = calculationService;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployeesAsync();

            return _mappingService.MapEmployeeListToDto(employees);
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            var employee = await _employeeService.GetEmployeeByIdAsync(selectedEmployeeId);

            //get the total salary budget for the company
            var totalSalary = await _calculationService.GetTotalSalaryOfTotalEmployeesAsync();

            //calculate the bonus allocation for the employee
            var bonusPercentage = _calculationService.CalculateEmployeeBonusPercentage(employee.Salary, totalSalary);
            int bonusAllocation = _calculationService.CalculateEmployeeBonus(bonusPercentage, bonusPoolAmount);

            return _mappingService.MapBonusPoolCalculatorResultToDto(employee, bonusAllocation);
        }
    }
}
