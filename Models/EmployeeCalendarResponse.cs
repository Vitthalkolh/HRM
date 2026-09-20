namespace HRM.API.Models
{
    public class EmployeeCalendarResponse
    {
        public DateTime CalendarDate { get; set; }

        public string EventType { get; set; }

        public int? UserId { get; set; }

        public string? EmployeeName { get; set; }

        public string? HolidayName { get; set; }
    }
}