using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Management_System_Net_Core_API.Models
{
    public class JobHistory
    {
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
