namespace HRM.API.Models
{
    public class CompanyHolidayResponse
    {
        public int Id { get; set; }
        public DateTime HolidayDate { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}