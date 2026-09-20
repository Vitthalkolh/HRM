using HRM.API.Helpers;
using HRM.API.Models;
using HRM.API.Models.Email;
using Microsoft.Data.SqlClient;

namespace HRM.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AdoHelper _adoHelper;

        public UserRepository(AdoHelper adoHelper)
        {
            _adoHelper = adoHelper;
        }

        public LoginResponse? Login(string username, string password)
        {
            LoginResponse? loginResponse = null;

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_USER_LOGIN",
                    new SqlParameter("@USERNAME", username),
                    new SqlParameter("@PASSWORD_HASH", password)
                );

            using SqlDataReader reader = _adoHelper.ExecuteReader(command);

            if (reader.Read())
            {
                loginResponse = new LoginResponse
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Username = reader["USERNAME"].ToString(),
                    FirstName = reader["FIRST_NAME"].ToString(),
                    LastName = reader["LAST_NAME"].ToString(),
                    Email = reader["EMAIL"].ToString(),
                    IsAdmin = Convert.ToBoolean(reader["IS_ADMIN"]),
                    IsActive = Convert.ToBoolean(reader["IS_ACTIVE"])
                };
            }

            return loginResponse;
        }
        public List<UserResponse> GetAllUsers()
        {
            List<UserResponse> users = new List<UserResponse>();
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_USERS"
                );

            using SqlDataReader reader = _adoHelper.ExecuteReader(command);

            while (reader.Read())
            {
                UserResponse user = new UserResponse
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Username = reader["USERNAME"].ToString(),
                    FirstName = reader["FIRST_NAME"].ToString(),
                    LastName = reader["LAST_NAME"].ToString(),
                    Email = reader["EMAIL"].ToString(),
                    MobileNo = reader["MOBILE_NO"].ToString(),
                    City = reader["CITY"].ToString(),
                    DateOfBirth = Convert.ToDateTime(reader["DATE_OF_BIRTH"]),
                    JoiningDate = Convert.ToDateTime(reader["JOINING_DATE"]),
                    CompanyLeavingDate = reader["COMPANY_LEAVING_DATE"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["COMPANY_LEAVING_DATE"]),
                    IsAdmin = Convert.ToBoolean(reader["IS_ADMIN"]),
                    IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]),
                    CreatedDate = Convert.ToDateTime(reader["CREATED_DATE"])
                };

                users.Add(user);
            }

            return users;
        }
        public int CreateUser(CreateUserRequest request, int createdBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_CREATE_USER",
                    new SqlParameter("@USERNAME", request.Username),
                    new SqlParameter("@PASSWORD_HASH", request.Password),
                    new SqlParameter("@FIRST_NAME", request.FirstName),
                    new SqlParameter("@LAST_NAME", request.LastName),
                    new SqlParameter("@EMAIL", request.Email),
                    new SqlParameter("@MOBILE_NO", request.MobileNo),
                    new SqlParameter("@CITY", request.City),
                    new SqlParameter("@DATE_OF_BIRTH", request.DateOfBirth),
                    new SqlParameter("@JOINING_DATE", request.JoiningDate),
                    new SqlParameter(
                        "@COMPANY_LEAVING_DATE",
                        request.CompanyLeavingDate ?? (object)DBNull.Value),
                    new SqlParameter("@IS_ADMIN", request.IsAdmin),
                    new SqlParameter("@IS_ACTIVE", request.IsActive),
                    new SqlParameter("@CREATED_BY", createdBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }
        public int UpdateUser(int id,UpdateUserRequest request, int changedBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_UPDATE_USER",
                    new SqlParameter("@ID", id),
                    new SqlParameter("@FIRST_NAME", request.FirstName),
                    new SqlParameter("@LAST_NAME", request.LastName),
                    new SqlParameter("@EMAIL", request.Email),
                    new SqlParameter("@MOBILE_NO", request.MobileNo),
                    new SqlParameter("@CITY", request.City),
                    new SqlParameter("@DATE_OF_BIRTH", request.DateOfBirth),
                    new SqlParameter("@JOINING_DATE", request.JoiningDate),
                    new SqlParameter(
                        "@COMPANY_LEAVING_DATE",
                        request.CompanyLeavingDate ?? (object)DBNull.Value),
                    new SqlParameter("@IS_ADMIN", request.IsAdmin),
                    new SqlParameter("@IS_ACTIVE", request.IsActive),
                    new SqlParameter("@CHANGED_BY", changedBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }
        public int DeleteUser(int id, int changedBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_DELETE_USER",
                    new SqlParameter("@ID", id),
                    new SqlParameter("@CHANGED_BY", changedBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }

        public void AllocateEmployeeLeave(int userId,int year,  int createdBy,int? leaveTypeId = null,decimal? entitlement = null)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command = _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_ALLOCATE_EMPLOYEE_LEAVE",
                    new SqlParameter("@USER_ID", userId),
                    new SqlParameter("@YEAR", year),
                    new SqlParameter("@CREATED_BY", createdBy),
                    new SqlParameter(
                        "@LEAVE_TYPE_ID",
                        leaveTypeId ?? (object)DBNull.Value),
                    new SqlParameter(
                        "@ENTITLEMENT",
                        entitlement ?? (object)DBNull.Value)
                );

            _adoHelper.ExecuteScalar(command);
        }
        public int CreateCompanyHoliday(DateTime holidayDate,int leaveTypeId,string name,int createdBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_CREATE_COMPANY_HOLIDAY",
                    new SqlParameter("@HOLIDAY_DATE", holidayDate),
                    new SqlParameter("@LEAVE_TYPE_ID", leaveTypeId),
                    new SqlParameter("@NAME", name),
                    new SqlParameter("@CREATED_BY", createdBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }
        public List<CompanyHolidayResponse> GetCompanyHolidays()
        {
            List<CompanyHolidayResponse> holidays = new List<CompanyHolidayResponse>();

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_COMPANY_HOLIDAYS"
                );

            using SqlDataReader reader = _adoHelper.ExecuteReader(command);

            while (reader.Read())
            {
                holidays.Add(new CompanyHolidayResponse
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    HolidayDate = Convert.ToDateTime(reader["HOLIDAY_DATE"]),
                    LeaveTypeId = Convert.ToInt32(reader["LEAVE_TYPE_ID"]),
                    LeaveTypeName = reader["LEAVE_TYPE_NAME"].ToString(),
                    Name = reader["NAME"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IS_ACTIVE"])
                });
            }

            return holidays;
        }
        public int ApplyEmployeeLeave(int userId,ApplyLeaveRequest request,int createdBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_APPLY_EMPLOYEE_LEAVE",
                    new SqlParameter("@USER_ID", userId),
                    new SqlParameter("@LEAVE_TYPE_ID", request.LeaveTypeId),
                    new SqlParameter("@FROM_DATE", request.FromDate),
                    new SqlParameter("@TO_DATE", request.ToDate),
                    new SqlParameter("@IS_HALF_DAY", request.IsHalfDay),
                    new SqlParameter("@REASON", request.Reason),
                    new SqlParameter("@CREATED_BY", createdBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }
        public int UpdateEmployeeLeaveStatus(
    int id,
    int statusId,
    int changedBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_UPDATE_EMPLOYEE_LEAVE_STATUS",
                    new SqlParameter("@ID", id),
                    new SqlParameter("@STATUS_ID", statusId),
                    new SqlParameter("@CHANGED_BY", changedBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }

        public List<EmployeeLeaveResponse> GetEmployeeLeaves(
    int? userId,
    int? statusId)
        {
            List<EmployeeLeaveResponse> leaves =
                new List<EmployeeLeaveResponse>();

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_EMPLOYEE_LEAVES",
                    new SqlParameter(
                        "@USER_ID",
                        userId ?? (object)DBNull.Value),
                    new SqlParameter(
                        "@STATUS_ID",
                        statusId ?? (object)DBNull.Value)
                );

            using SqlDataReader reader =
                _adoHelper.ExecuteReader(command);

            while (reader.Read())
            {
                leaves.Add(new EmployeeLeaveResponse
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    UserId = Convert.ToInt32(reader["USER_ID"]),
                    EmployeeName = reader["EMPLOYEE_NAME"].ToString(),

                    LeaveTypeId =
                        Convert.ToInt32(reader["LEAVE_TYPE_ID"]),

                    LeaveTypeName =
                        reader["LEAVE_TYPE_NAME"].ToString(),

                    FromDate =
                        Convert.ToDateTime(reader["FROM_DATE"]),

                    ToDate =
                        Convert.ToDateTime(reader["TO_DATE"]),

                    IsHalfDay =
                        Convert.ToBoolean(reader["IS_HALF_DAY"]),

                    TotalDays =
                        Convert.ToDecimal(reader["TOTAL_DAYS"]),

                    Reason =
                        reader["REASON"].ToString(),

                    StatusId =
                        Convert.ToInt32(reader["STATUS_ID"]),

                    StatusName =
                        reader["STATUS_NAME"].ToString(),

                    ApprovedBy =
                        reader["APPROVED_BY"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["APPROVED_BY"]),

                    ApprovedDate =
                        reader["APPROVED_DATE"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["APPROVED_DATE"]),

                    CancelledBy =
                        reader["CANCELLED_BY"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["CANCELLED_BY"]),

                    CancelledDate =
                        reader["CANCELLED_DATE"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(reader["CANCELLED_DATE"]),

                    CreatedDate =
                        Convert.ToDateTime(reader["CREATED_DATE"])
                });
            }

            return leaves;
        }
        public int CancelEmployeeLeave(
    int id,
    int cancelledBy)
        {
            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_CANCEL_EMPLOYEE_LEAVE",
                    new SqlParameter("@ID", id),
                    new SqlParameter("@CANCELLED_BY", cancelledBy)
                );

            object? result = _adoHelper.ExecuteScalar(command);

            return Convert.ToInt32(result);
        }
        public List<EmployeeLeaveBalanceResponse> GetEmployeeLeaveBalance(
    int userId,
    int year)
        {
            List<EmployeeLeaveBalanceResponse> balances =
                new List<EmployeeLeaveBalanceResponse>();

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_EMPLOYEE_LEAVE_BALANCE",
                    new SqlParameter("@USER_ID", userId),
                    new SqlParameter("@YEAR", year)
                );

            using SqlDataReader reader =
                _adoHelper.ExecuteReader(command);

            while (reader.Read())
            {
                balances.Add(new EmployeeLeaveBalanceResponse
                {
                    LeaveTypeId =
                        Convert.ToInt32(reader["LEAVE_TYPE_ID"]),

                    LeaveTypeName =
                        reader["LEAVE_TYPE_NAME"].ToString(),

                    Entitlement =
                        Convert.ToDecimal(reader["ENTITLEMENT"]),

                    Used =
                        Convert.ToDecimal(reader["USED"]),

                    Remaining =
                        Convert.ToDecimal(reader["REMAINING"])
                });
            }

            return balances;
        }      
        public List<EmployeeCalendarResponse> GetEmployeeCalendar(
    int year,
    int month)
        {
            List<EmployeeCalendarResponse> calendar =
                new List<EmployeeCalendarResponse>();

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_EMPLOYEE_CALENDAR",
                    new SqlParameter("@YEAR", year),
                    new SqlParameter("@MONTH", month)
                );

            using SqlDataReader reader =
                _adoHelper.ExecuteReader(command);

            while (reader.Read())
            {
                calendar.Add(new EmployeeCalendarResponse
                {
                    CalendarDate =
                        Convert.ToDateTime(reader["CALENDAR_DATE"]),

                    EventType =
                        reader["EVENT_TYPE"].ToString(),

                    UserId =
                        reader["USER_ID"] == DBNull.Value
                            ? null
                            : Convert.ToInt32(reader["USER_ID"]),

                    EmployeeName =
                        reader["EMPLOYEE_NAME"] == DBNull.Value
                            ? null
                            : reader["EMPLOYEE_NAME"].ToString(),

                    HolidayName =
                        reader["HOLIDAY_NAME"] == DBNull.Value
                            ? null
                            : reader["HOLIDAY_NAME"].ToString()
                });
            }

            return calendar;
        }
        public EmailNotificationTemplate GetEmailNotificationTemplate(
    EmailNotificationType notificationType)
        {
            EmailNotificationTemplate template = null;

            using SqlConnection connection = _adoHelper.GetConnection();

            using SqlCommand command =
                _adoHelper.CreateStoredProcedureCommand(
                    connection,
                    "SP_GET_AUTO_EMAIL_NOTIFICATION",
                    new SqlParameter("@ID", (int)notificationType)
                );

            using SqlDataReader reader =
                _adoHelper.ExecuteReader(command);

            if (reader.Read())
            {
                template = new EmailNotificationTemplate
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Code = reader["CODE"].ToString(),
                    Name = reader["NAME"].ToString(),
                    Subject = reader["SUBJECT"].ToString(),
                    Body = reader["BODY"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]),
                    ToEmail = reader["TO_EMAIL"] == DBNull.Value? null: reader["TO_EMAIL"].ToString(),
                    CcEmail = reader["CC_EMAIL"] == DBNull.Value? null: reader["CC_EMAIL"].ToString(),
                    BccEmail = reader["BCC_EMAIL"] == DBNull.Value    ? null    : reader["BCC_EMAIL"].ToString(),
                };
            }

            return template;
        }
    }
}