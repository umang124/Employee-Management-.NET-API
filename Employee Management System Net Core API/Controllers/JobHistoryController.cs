using Employee_Management_System_Net_Core_API.Data;
using Employee_Management_System_Net_Core_API.Models;
using Employee_Management_System_Net_Core_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Employee_Management_System_Net_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public JobHistoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetJobHistories()
        {
            var jobHistoryList = await _db.JobHistories.Include("Employee").ToListAsync();
            List<JobHistoryGetDTO> jobHistoryGetDTOs = new();

            foreach (var jobHistory in jobHistoryList)
            {
                var jobHistoryGetDTO = new JobHistoryGetDTO()
                {
                    EmployeeName = jobHistory.Employee.FirstName + " " + jobHistory.Employee.LastName,
                    Email = jobHistory.Employee.Email,
                    Department = jobHistory.Department,
                    Position = jobHistory.Position,
                    StartDate = jobHistory.StartDate,
                    EndDate = jobHistory.EndDate
                };

                jobHistoryGetDTOs.Add(jobHistoryGetDTO);
            }
            return Ok(jobHistoryGetDTOs);
        }

        [HttpGet("{id:int}", Name = "GetJobHistory")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetJobHistory(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var jobHistory = await _db.JobHistories.Include("Employee")
                                    .FirstOrDefaultAsync(x => x.Id == id);
            if (jobHistory == null)
            {
                return NotFound();
            }
            var jobHistoryGetDTO = new JobHistoryGetDTO()
            {
                EmployeeName = jobHistory.Employee.FirstName + " " + jobHistory.Employee.LastName,
                Email = jobHistory.Employee.Email,
                Department = jobHistory.Department,
                Position = jobHistory.Position,
                StartDate = jobHistory.StartDate,
                EndDate = jobHistory.EndDate
            };

            return Ok(jobHistoryGetDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateJobHistory([FromBody] JobHistoryAddDTO jobHistoryAdd)
        {
            if (jobHistoryAdd == null)
            {
                return BadRequest();
            }

            if (!_db.Employees.Any(x => x.Id == jobHistoryAdd.EmployeeId))
            {
                ModelState.AddModelError("UnknownEmployee", "Invalid Employee Id");
                return BadRequest(ModelState);
            }

            JobHistory model = new JobHistory()
            {
                EmployeeId = jobHistoryAdd.EmployeeId,
                Department = jobHistoryAdd.Department,
                Position = jobHistoryAdd.Position,
                StartDate = jobHistoryAdd.StartDate,
                EndDate = jobHistoryAdd.EndDate
            };

            await _db.JobHistories.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetJobHistory", new { id = model.Id }, jobHistoryAdd);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateJobHistory(int id, [FromBody] JobHistoryUpdateDTO jobHistoryUpdate)
        {
            if (id != jobHistoryUpdate.Id || jobHistoryUpdate == null)
            {
                return BadRequest();
            }

            var getJobHistory = await _db.JobHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (getJobHistory == null)
            {
                return NotFound();
            }           

            if (_db.JobHistories.First(x => x.Id == jobHistoryUpdate.Id).EndDate != null)
            {
                ModelState.AddModelError("EndDateAlreadyAdded", "End Date Already Added");
                return BadRequest(ModelState);
            }

            getJobHistory.EndDate = jobHistoryUpdate.EndDate;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteJobHistory(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var getJobHistory = await _db.JobHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (getJobHistory == null)
            {
                return NotFound();
            }
            _db.JobHistories.Remove(getJobHistory);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
