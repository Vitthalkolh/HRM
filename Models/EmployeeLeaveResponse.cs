namespace HRM.API.Models
{
    public class EmployeeLeaveResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsHalfDay { get; set; }
        public decimal TotalDays { get; set; }
        public string Reason { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}