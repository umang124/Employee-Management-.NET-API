using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System_Net_Core_API.Models
{
    public class PerformanceEvaluation
    {
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
