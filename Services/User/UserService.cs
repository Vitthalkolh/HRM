using HRM.API.Models;
using HRM.API.Models.Email;
using HRM.API.Repository;

namespace HRM.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResponse? Login(string username, string password)
        {
            return _userRepository.Login(username, password);
        }
        public List<UserResponse> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public int CreateUser(CreateUserRequest request, int createdBy)
        {
            int userId = _userRepository.CreateUser(request, createdBy);

            if (userId == -1)
            {
                return -1;
            }
            int year = DateTime.Now.Year;

            AllocateEmployeeLeave(userId,year,createdBy);

            return userId;
        }

        public int UpdateUser(int id,UpdateUserRequest request, int changedBy)
        {
            return _userRepository.UpdateUser(id,request, changedBy);
        }
        public int DeleteUser(int id, int changedBy)
        {
            return _userRepository.DeleteUser(id, changedBy);
        }
        public void AllocateEmployeeLeave(int userId,int year,int createdBy,int? leaveTypeId = null,decimal? entitlement = null)
        {
            _userRepository.AllocateEmployeeLeave(userId,year,createdBy,leaveTypeId,entitlement);
        }
        public int CreateCompanyHoliday(DateTime holidayDate,int leaveTypeId,string name,int createdBy)
        {
            return _userRepository.CreateCompanyHoliday(holidayDate,leaveTypeId,name,createdBy);
        }
        public List<CompanyHolidayResponse> GetCompanyHolidays()
        {
            return _userRepository.GetCompanyHolidays();
        }
        public int ApplyEmployeeLeave(int userId,ApplyLeaveRequest request,int createdBy)
        {
            return _userRepository.ApplyEmployeeLeave(userId,request,createdBy);
        }
        public int UpdateEmployeeLeaveStatus(
    int id,
    int statusId,
    int changedBy)
        {
            return _userRepository.UpdateEmployeeLeaveStatus(
                id,
                statusId,
                changedBy);
        }
        public List<EmployeeLeaveResponse> GetEmployeeLeaves(
    int? userId,
    int? statusId)
        {
            return _userRepository.GetEmployeeLeaves(
                userId,
                statusId);
        }
        public int CancelEmployeeLeave(
    int id,
    int cancelledBy)
        {
            return _userRepository.CancelEmployeeLeave(
                id,
                cancelledBy);
        }
        public List<EmployeeLeaveBalanceResponse> GetEmployeeLeaveBalance(
    int userId,
    int year)
        {
            return _userRepository.GetEmployeeLeaveBalance(
                userId,
                year);
        }
        public List<EmployeeCalendarResponse> GetEmployeeCalendar(
    int year,
    int month)
        {
            return _userRepository.GetEmployeeCalendar(year, month);
        }
        public EmailNotificationTemplate GetEmailNotificationTemplate(
    EmailNotificationType notificationType)
        {
            return _userRepository.GetEmailNotificationTemplate(
                notificationType);
        }
    }
}