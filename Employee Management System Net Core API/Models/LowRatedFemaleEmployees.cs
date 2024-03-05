using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Models
{
    [Keyless]
    public class LowRatedFemaleEmployees
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
