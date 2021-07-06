using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees
                                    .Include(e => e.Department)
                                    .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _dbContext.Employees
                                    .Include(e => e.Department)
                                    .FirstOrDefaultAsync(item => item.Id == employeeId);
        }

        public async Task<int> GetTotalSalaryOfTotalEmployeesAsync()
        {
            return await _dbContext.Employees.SumAsync(item => item.Salary);
        }
    }
}
