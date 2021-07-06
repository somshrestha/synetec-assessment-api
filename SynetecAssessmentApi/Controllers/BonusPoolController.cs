using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Constants;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _service;

        public BonusPoolController(IBonusPoolService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetEmployeesAsync());
        }

        [HttpPost("CalculateBonus")]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.SelectedEmployeeId == default)
                return BadRequest(ExceptionMessages.EmployeeIdIsNotValid(request.SelectedEmployeeId));

            try
            {
                var result = await _service.CalculateAsync(request.TotalBonusPoolAmount, request.SelectedEmployeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
