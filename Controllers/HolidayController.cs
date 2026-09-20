using HRM.API.Models;
using HRM.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IUserService _userService;

        public HolidayController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateHoliday(DateTime holidayDate,int leaveTypeId,string name)
        {
            int createdBy = 1; // Temporary admin user ID

            int holidayId = _userService.CreateCompanyHoliday(holidayDate,leaveTypeId,name,createdBy);

            if (holidayId == -1)
            {
                return BadRequest(new
                {
                    Message = "Holiday already exists for this date"
                });
            }
            if (holidayId == -2)
            {
                return BadRequest(new
                {
                    Message = "Holiday date must be a future date"
                });
            }

            return Ok(new
            {
                Id = holidayId,
                Message = "Holiday created successfully"
            });
        }

        [HttpGet]
        public IActionResult GetCompanyHolidays()
        {
            List<CompanyHolidayResponse> holidays = _userService.GetCompanyHolidays();

            return Ok(holidays);
        }
    }
}