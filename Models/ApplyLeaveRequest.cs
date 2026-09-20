namespace HRM.API.Models
{
    public class ApplyLeaveRequest
    {
        public int LeaveTypeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsHalfDay { get; set; }

        public string Reason { get; set; }
    }
}