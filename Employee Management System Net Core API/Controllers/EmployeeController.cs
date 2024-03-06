using Azure;
using Employee_Management_System_Net_Core_API.Data;
using Employee_Management_System_Net_Core_API.Models;
using Employee_Management_System_Net_Core_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Employee_Management_System_Net_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployees()
        {
            var employeeList = await _db.Employees.Include("Gender").ToListAsync();
            List<EmployeeGetDTO> employeeGetDTOs = new();

            foreach (var employee in employeeList)
            {
                var employeeGetDTO = new EmployeeGetDTO()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Gender = employee.Gender.Name,
                    Email = employee.Email,
                    Address = employee.Address,
                    DateOfBirth = employee.DateOfBirth,
                    ContactNumber = employee.ContactNumber
                };

                employeeGetDTOs.Add(employeeGetDTO);
            }

            return Ok(employeeGetDTOs);
        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var employee = await _db.Employees.Include("Gender")
                                    .FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeGetDTO = new EmployeeGetDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender.Name,
                Email = employee.Email,
                Address = employee.Address,
                DateOfBirth = employee.DateOfBirth,
                ContactNumber = employee.ContactNumber
            };

            return Ok(employeeGetDTO);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO employeeUpdate)
        {
            if (id != employeeUpdate.Id || employeeUpdate == null)
            {
                return BadRequest();
            }

            var getEmployee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (getEmployee == null)
            {
                return NotFound();
            }

            Regex regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(employeeUpdate.ContactNumber))
            {
                ModelState.AddModelError("ContactNumberError", "Please enter 10 digits number");
                return BadRequest(ModelState);
            }

            if (!double.TryParse(employeeUpdate.ContactNumber, out _))
            {
                ModelState.AddModelError("ContactNumberError", "Invalid Phone Number");
                return BadRequest(ModelState);
            }

            if (_db.Employees.Where(x => x.Id != id).
                        Any(x => x.ContactNumber == employeeUpdate.ContactNumber))
            {
                ModelState.AddModelError("ContactNumberError", "Phone number already exists");
                return BadRequest(ModelState);
            }

            if (_db.Employees.Where(x => x.Id != id).
                        Any(x => x.Email == employeeUpdate.Email))
            {
                ModelState.AddModelError("EmailError", "Email already exists");
                return BadRequest(ModelState);
            }

            getEmployee.Email = employeeUpdate.Email;
            getEmployee.ContactNumber = employeeUpdate.ContactNumber;
            getEmployee.Address = employeeUpdate.Address;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var getEmployee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (getEmployee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(getEmployee);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeAddDTO employeeAdd)
        {
            if (employeeAdd == null)
            {
                return BadRequest();
            }

            if (!_db.Genders.Any(x => x.Id == employeeAdd.GenderId))
            {
                ModelState.AddModelError("GenderError", "Invalid Gender Id");
                return BadRequest(ModelState);
            }

            Regex regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(employeeAdd.ContactNumber))
            {
                ModelState.AddModelError("ContactNumberError", "Please enter 10 digits number");
                return BadRequest(ModelState);
            }

            if (!double.TryParse(employeeAdd.ContactNumber, out _))
            {
                ModelState.AddModelError("ContactNumberError", "Invalid Phone Number");
                return BadRequest(ModelState);
            }

            if (_db.Employees.Any(x => x.ContactNumber == employeeAdd.ContactNumber))
            {
                ModelState.AddModelError("ContactNumberError", "Phone Number already exists");
                return BadRequest(ModelState);
            }

            if (_db.Employees.Any(x => x.Email == employeeAdd.Email))
            {
                ModelState.AddModelError("EmailError", "Email already exists");
                return BadRequest(ModelState);
            }

            Employee model = new Employee()
            {
                FirstName = employeeAdd.FirstName,
                LastName = employeeAdd.LastName,
                DateOfBirth = employeeAdd.DateOfBirth,
                GenderId = employeeAdd.GenderId,
                ContactNumber = employeeAdd.ContactNumber,
                Email = employeeAdd.Email,
                Address = employeeAdd.Address
            };

            await _db.Employees.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetEmployee", new { id = model.Id }, employeeAdd);
        }

        [HttpGet("MaleEmployeeWithHighRating")]
        public async Task<IActionResult> GetHighRatedMaleEmployees()
        {
            var highRatedMaleEmployees = await _db.HighlyRatedMaleEmployees.FromSqlRaw("EXEC GetHighlyRatedMaleEmployees").ToListAsync();
            return Ok(highRatedMaleEmployees);
        }

        [HttpGet("FemaleEmployeeWithLowRating")]
        public async Task<IActionResult> GetLowRatedFemaleEmployees()
        {
            var lowRatedFemaleEmployees = await _db.LowRatedFemaleEmployees.FromSqlRaw("EXEC GetLowRatedFemaleEmployees").ToListAsync();
            return Ok(lowRatedFemaleEmployees);
        }

        [HttpGet("EmployeesBetween30And35Age")]
        public async Task<IActionResult> GetEmployeesBetween30And35Age()
        {
            var employeesBetween30And35 = await _db.EmployeesBetween30And35Ages.FromSqlRaw("EXEC GetEmployeesBetween30And35").ToListAsync();
            return Ok(employeesBetween30And35);
        }

        [HttpGet("EmployeesWithCurrentJobPosition")]
        public async Task<IActionResult> GetEmployeesWithCurrentJobPosition()
        {
            var employeesWithCurrentJobPosition = await _db.EmployeesWithCurrentJobPositions.FromSqlRaw("EXEC EmployeesWithCurrentJobPosition").ToListAsync();
            return Ok(employeesWithCurrentJobPosition);
        }
    }
}
