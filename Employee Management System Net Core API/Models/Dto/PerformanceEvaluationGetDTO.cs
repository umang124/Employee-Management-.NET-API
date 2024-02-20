using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System_Net_Core_API.Models.Dto
{
    public class PerformanceEvaluationGetDTO
    {
        public int Id { get; set; }
        public DateTime EvaluationDate { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
    }
}
