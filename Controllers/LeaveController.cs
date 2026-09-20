using HRM.API.Models;
using HRM.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IUserService _userService;

        public LeaveController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult ApplyLeave(ApplyLeaveRequest request)
        {
            int userId = 3;    // Temporary employee user ID
            int createdBy = 3; // Temporary employee user ID

            int leaveId = _userService.ApplyEmployeeLeave(
                userId,
                request,
                createdBy);

            if (leaveId == -1)
            {
                return BadRequest(new
                {
                    Message = "From date cannot be greater than To date"
                });
            }

            if (leaveId == -2)
            {
                return BadRequest(new
                {
                    Message = "Leave cannot cross calendar years"
                });
            }

            if (leaveId == -3)
            {
                return BadRequest(new
                {
                    Message = "Half-day leave is allowed only for a single date"
                });
            }

            if (leaveId == -4)
            {
                return BadRequest(new
                {
                    Message = "Leave balance not found for this employee"
                });
            }

            if (leaveId == -5)
            {
                return BadRequest(new
                {
                    Message = "Leave already exists for one or more selected dates"
                });
            }

            if (leaveId == -6)
            {
                return BadRequest(new
                {
                    Message = "Selected dates contain no working days"
                });
            }

            if (leaveId == -7)
            {
                return BadRequest(new
                {
                    Message = "Insufficient leave balance"
                });
            }

            return Ok(new
            {
                Id = leaveId,
                Message = "Leave applied successfully"
            });
        }
        [HttpPut("status/{id}")]
        public IActionResult UpdateLeaveStatus(int id,int statusId)
        {
            int changedBy = 1; // Temporary admin user ID

            int result = _userService.UpdateEmployeeLeaveStatus(
                id,
                statusId,
                changedBy);

            if (result == -1)
            {
                return BadRequest(new
                {
                    Message = "Leave not found or already processed"
                });
            }

            if (result == -2)
            {
                return BadRequest(new
                {
                    Message = "Leave balance not found"
                });
            }

            if (result == -3)
            {
                return BadRequest(new
                {
                    Message = "Insufficient leave balance"
                });
            }

            if (result == -4)
            {
                return BadRequest(new
                {
                    Message = "Invalid leave status"
                });
            }

            string message = statusId == 2
                ? "Leave approved successfully"
                : "Leave rejected successfully";

            return Ok(new
            {
                Id = result,
                Message = message
            });
        }
        [HttpGet]
        public IActionResult GetEmployeeLeaves(
    int? userId,
    int? statusId)
        {
            List<EmployeeLeaveResponse> leaves =
                _userService.GetEmployeeLeaves(
                    userId,
                    statusId);

            return Ok(leaves);
        }

        [HttpPut("cancel/{id}")]
        public IActionResult CancelLeave(int id)
        {
            int cancelledBy = 1; // Temporary admin user ID

            int result = _userService.CancelEmployeeLeave(
                id,
                cancelledBy);

            if (result == -1)
            {
                return BadRequest(new
                {
                    Message = "Leave not found or leave is not approved"
                });
            }

            if (result == -2)
            {
                return BadRequest(new
                {
                    Message = "Leave balance not found"
                });
            }

            if (result == -3)
            {
                return BadRequest(new
                {
                    Message = "Invalid leave balance"
                });
            }

            return Ok(new
            {
                Id = result,
                Message = "Leave cancelled successfully"
            });
        }
        [HttpGet("balance")]
        public IActionResult GetEmployeeLeaveBalance(
    int userId,
    int year)
        {
            List<EmployeeLeaveBalanceResponse> balances =
                _userService.GetEmployeeLeaveBalance(
                    userId,
                    year);

            return Ok(balances);
        }
        [HttpGet("calendar")]
        public IActionResult GetEmployeeCalendar(int year,int month)
        {
            if (month < 1 || month > 12)
            {
                return BadRequest(new
                {
                    Message = "Invalid month"
                });
            }

            int currentYear = DateTime.Today.Year;
            int minimumYear = currentYear - 15;

            if (year < minimumYear || year > currentYear)
            {
                return BadRequest(new
                {
                    Message =
                        $"Year must be between {minimumYear} and {currentYear}"
                });
            }

            List<EmployeeCalendarResponse> calendar =
                _userService.GetEmployeeCalendar(year, month);

            return Ok(calendar);
        }
    }
}