using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Models
{
    [Keyless]
    public class EmployeesWithCurrentJobPosition
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
