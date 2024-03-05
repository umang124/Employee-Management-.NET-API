using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Models
{
    [Keyless]
    public class EmployeesBetween30And35Age
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
