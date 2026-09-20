using HRM.API.Models;
using HRM.API.Models.Email;

namespace HRM.API.Services
{
    public interface IUserService
    {
        LoginResponse? Login(string username, string password);
        List<UserResponse> GetAllUsers();
        int CreateUser(CreateUserRequest request, int createdBy);
        int UpdateUser(int id,UpdateUserRequest request, int changedBy);
        int DeleteUser(int id, int changedBy);
        void AllocateEmployeeLeave(int userId,int year,int createdBy,int? leaveTypeId = null,decimal? entitlement = null);
        int CreateCompanyHoliday(DateTime holidayDate,int leaveTypeId,string name,int createdBy);

        List<CompanyHolidayResponse> GetCompanyHolidays();
        int ApplyEmployeeLeave(int userId,ApplyLeaveRequest request,int createdBy);

        int UpdateEmployeeLeaveStatus(
            int id,
            int statusId,
            int changedBy);
        List<EmployeeLeaveResponse> GetEmployeeLeaves(
    int? userId,
    int? statusId);

        int CancelEmployeeLeave(
    int id,
    int cancelledBy);
        List<EmployeeLeaveBalanceResponse> GetEmployeeLeaveBalance(
    int userId,
    int year);
        List<EmployeeCalendarResponse> GetEmployeeCalendar(
    int year,
    int month);
        EmailNotificationTemplate GetEmailNotificationTemplate(
    EmailNotificationType notificationType);
    }
}