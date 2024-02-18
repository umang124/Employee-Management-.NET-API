using System;

namespace Employee_Management_System_Net_Core_API.Models
{
    public class JobHistory
    {
        //        HistoryID(Primary Key)
        //EmployeeID(Foreign Key referencing Employees.EmployeeID)
        //Department
        //Position
        //StartDate
        //EndDate(nullable for current positions)

        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
