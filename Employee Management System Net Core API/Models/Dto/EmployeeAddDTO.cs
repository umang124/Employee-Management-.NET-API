﻿namespace Employee_Management_System_Net_Core_API.Models.Dto
{
    public class EmployeeAddDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
