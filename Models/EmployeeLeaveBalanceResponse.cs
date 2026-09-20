namespace HRM.API.Models
{
    public class EmployeeLeaveBalanceResponse
    {
        public int LeaveTypeId { get; set; }

        public string LeaveTypeName { get; set; }

        public decimal Entitlement { get; set; }

        public decimal Used { get; set; }

        public decimal Remaining { get; set; }
    }
}