using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Services;

namespace SynetecAssessmentApiUnitTests.Services
{
    public class CalculationServiceTests
    {
        protected CalculationService _subject;
        protected Mock<IEmployeeRepository> _employeeRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _subject = new CalculationService(_employeeRepositoryMock.Object);
        }

        [Test]
        public void GetTotalSalaryOfTotalEmployeesAsync_Should_Return_Correct_Result()
        {
            var totalSalaryofTotalEmployees = 100000;
            var expectedResult = 100000;

            _employeeRepositoryMock.Setup(r => r.GetTotalSalaryOfTotalEmployeesAsync()).ReturnsAsync(totalSalaryofTotalEmployees);

            Assert.AreEqual(expectedResult, totalSalaryofTotalEmployees);
        }

        [Test]
        public void CalculateEmployeeBonusPercentage_Should_Return_Zero_When_Employee_Salary_Is_Zero()
        {
            var employeeSalary = 0;
            var totalSalary = 500000;
            var expectedResult = 0;

            var result = _subject.CalculateEmployeeBonusPercentage(employeeSalary, totalSalary);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CalculateEmployeeBonusPercentage_Should_Return_Correct_Result()
        {
            var employeeSalary = 50000;
            var totalSalary = 500000;
            var expectedResult = 0.1M;

            var result = _subject.CalculateEmployeeBonusPercentage(employeeSalary, totalSalary);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CalculateEmployeeBonus_Should_Return_Zero_When_Bonus_Percentage_Is_Zero()
        {
            var bonusPercentage = 0;
            var bonusPoolAmount = 1000;
            var expectedResult = 0;

            var result = _subject.CalculateEmployeeBonus(bonusPercentage, bonusPoolAmount);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
