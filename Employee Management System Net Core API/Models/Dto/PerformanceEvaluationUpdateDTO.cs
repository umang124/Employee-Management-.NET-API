using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System_Net_Core_API.Models.Dto
{
    public class PerformanceEvaluationUpdateDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
