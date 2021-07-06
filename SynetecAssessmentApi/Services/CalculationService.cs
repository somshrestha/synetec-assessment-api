using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Services.Interfaces;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CalculationService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> GetTotalSalaryOfTotalEmployeesAsync()
        {
            return await _employeeRepository.GetTotalSalaryOfTotalEmployeesAsync();
        }

        public decimal CalculateEmployeeBonusPercentage(decimal employeeSalary, decimal totalSalary)
        {
            return employeeSalary / totalSalary;
        }

        public int CalculateEmployeeBonus(decimal bonusPercentage, int bonusPoolAmount)
        {
            return (int)(bonusPercentage * bonusPoolAmount);
        }
    }
}
