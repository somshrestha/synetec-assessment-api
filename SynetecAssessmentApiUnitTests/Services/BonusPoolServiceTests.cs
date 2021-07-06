using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApiUnitTests.Services
{
    public class BonusPoolServiceTests
    {
        protected BonusPoolService _subject;
        protected Mock<IEmployeeService> _employeeServiceMock;
        protected Mock<IMappingService> _mappingServiceMock;
        protected Mock<ICalculationService> _calculationServiceMock;

        [SetUp]
        public void Setup()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _mappingServiceMock = new Mock<IMappingService>();
            _calculationServiceMock = new Mock<ICalculationService>();
            _subject = new BonusPoolService(_employeeServiceMock.Object,
                                                _mappingServiceMock.Object,
                                                _calculationServiceMock.Object);
        }

        [Test]
        public async Task GetEmployeesAsync_Should_Call_EmployeesService()
        {
            _employeeServiceMock.Setup(s => s.GetEmployeesAsync()).ReturnsAsync(It.IsAny<IEnumerable<Employee>>);

            var result = await _subject.GetEmployeesAsync();

            _employeeServiceMock.Verify(x => x.GetEmployeesAsync(), Times.Once);
        }

    }
}
