using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Models
{
    [Keyless]
    public class EmployeesAverageRating
    {
        public int EmployeeId { get; set; }
        public int AverageRating { get; set; }
    }
}
