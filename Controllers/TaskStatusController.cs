using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly taskManagerDbContext _context;

        public TaskStatusController(taskManagerDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllstatusTask")]
        public IActionResult GetAllstatusTask()
        {
            var taskStatuses = _context.TTaskStatuses.ToList();
            return Ok(taskStatuses);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTaskStatus(int id)
        {
            var taskStatus = _context.TTaskStatuses.FirstOrDefault(t => t.StatusId == id);

            if (taskStatus == null)
            {
                return NotFound();
            }

            return Ok(taskStatus);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTaskStatus([FromBody] TTaskStatus taskStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TTaskStatuses.Add(taskStatus);
            _context.SaveChanges();

            return CreatedAtAction("GetTaskStatus", new { id = taskStatus.StatusId }, taskStatus);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTaskStatus(int id, [FromBody] TTaskStatus taskStatus)
        {
            if (id != taskStatus.StatusId)
            {
                return BadRequest();
            }

            _context.Entry(taskStatus).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TTaskStatuses.Any(t => t.StatusId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTaskStatus(int id)
        {
            var taskStatus = _context.TTaskStatuses.FirstOrDefault(t => t.StatusId == id);

            if (taskStatus == null)
            {
                return NotFound();
            }

            _context.TTaskStatuses.Remove(taskStatus);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
