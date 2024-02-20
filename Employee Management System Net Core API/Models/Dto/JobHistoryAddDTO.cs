namespace Employee_Management_System_Net_Core_API.Models.Dto
{
    public class JobHistoryAddDTO
    {
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
