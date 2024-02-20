using Employee_Management_System_Net_Core_API.Data;
using Employee_Management_System_Net_Core_API.Models.Dto;
using Employee_Management_System_Net_Core_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System_Net_Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceEvaluationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PerformanceEvaluationController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPerformanceEvaluations()
        {
            var performanceEvaluationList = await _db.PerformanceEvaluations.Include("Employee").ToListAsync();
            List<PerformanceEvaluationGetDTO> performanceEvaluationGetDTOs = new();

            foreach (var performanceEvaluation in performanceEvaluationList)
            {
                var performanceEvaluationGetDTO = new PerformanceEvaluationGetDTO()
                {
                    Id = performanceEvaluation.Id,
                    EmployeeName = performanceEvaluation.Employee.FirstName + " " + performanceEvaluation.Employee.LastName,
                    Email = performanceEvaluation.Employee.Email,
                    EvaluationDate = performanceEvaluation.EvaluationDate,
                    Rating = performanceEvaluation.Rating,
                    Comments = performanceEvaluation.Comments
                };

                performanceEvaluationGetDTOs.Add(performanceEvaluationGetDTO);
            }
            return Ok(performanceEvaluationGetDTOs);
        }

        [HttpGet("{id:int}", Name = "GetPerformanceEvaluation")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPerformanceEvaluation(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var getPerformanceEvaluation = await _db.PerformanceEvaluations.Include("Employee")
                                    .FirstOrDefaultAsync(x => x.Id == id);
            if (getPerformanceEvaluation == null)
            {
                return NotFound();
            }
            var performanceEvaluationGetDTO = new PerformanceEvaluationGetDTO()
            {
                Id = getPerformanceEvaluation.Id,
                EmployeeName = getPerformanceEvaluation.Employee.FirstName + " " + getPerformanceEvaluation.Employee.LastName,
                Email = getPerformanceEvaluation.Employee.Email,
                EvaluationDate = getPerformanceEvaluation.EvaluationDate,
                Rating = getPerformanceEvaluation.Rating,
                Comments = getPerformanceEvaluation.Comments
            };

            return Ok(performanceEvaluationGetDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePerformanceEvaluation([FromBody] PerformanceEvaluationAddDTO performanceEvaluationAdd)
        {
            if (performanceEvaluationAdd == null)
            {
                return BadRequest();
            }

            if (!_db.Employees.Any(x => x.Id == performanceEvaluationAdd.EmployeeId))
            {
                ModelState.AddModelError("UnknownEmployee", "Invalid Employee Id");
                return BadRequest(ModelState);
            }

            PerformanceEvaluation model = new PerformanceEvaluation()
            {
                EmployeeId = performanceEvaluationAdd.EmployeeId,
                EvaluationDate = performanceEvaluationAdd.EvaluationDate,
                Rating = performanceEvaluationAdd.Rating,
                Comments = performanceEvaluationAdd.Comments
            };

            await _db.PerformanceEvaluations.AddAsync(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetPerformanceEvaluation", new { id = model.Id }, performanceEvaluationAdd);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePerformanceEvaluation(int id, [FromBody] PerformanceEvaluationUpdateDTO performanceEvaluationUpdate)
        {
            if (id != performanceEvaluationUpdate.Id || performanceEvaluationUpdate == null)
            {
                return BadRequest();
            }

            var getperformanceEvaluation = await _db.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
            if (getperformanceEvaluation == null)
            {
                return NotFound();
            }

            getperformanceEvaluation.Rating = performanceEvaluationUpdate.Rating;
            getperformanceEvaluation.Comments = performanceEvaluationUpdate.Comments;
            await _db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePerformanceEvaluation(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var getperformanceEvaluation = await _db.PerformanceEvaluations.FirstOrDefaultAsync(x => x.Id == id);
            if (getperformanceEvaluation == null)
            {
                return NotFound();
            }
            _db.PerformanceEvaluations.Remove(getperformanceEvaluation);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
