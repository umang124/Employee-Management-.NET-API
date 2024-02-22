namespace Employee_Management_System_Net_Core_API.Models.Dto
{
    public class JobHistoryGetDTO
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
