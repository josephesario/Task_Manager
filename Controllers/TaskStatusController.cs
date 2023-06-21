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


        [HttpGet("GetAllstatusTask")]
        public IActionResult GetAllstatusTask()
        {
            var taskStatuses = _context.TTaskStatuses.ToList();
            return Ok(taskStatuses);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskStatus(bool Status)
        {
            var taskStatus = _context.TTaskStatuses.FirstOrDefault(t => t.Status == Status);

            if (taskStatus == null)
            {
                return NotFound();
            }

            return Ok(taskStatus);
        }

        [HttpPost]
        public IActionResult CreateTaskStatus([FromBody] TTaskStatus taskStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TTaskStatuses.Add(taskStatus);
            _context.SaveChanges();

            return CreatedAtAction("GetTaskStatus", new { Status = taskStatus.Status }, taskStatus);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskStatus(bool Status, [FromBody] TTaskStatus taskStatus)
        {
            if (Status != taskStatus.Status)
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
                if (!_context.TTaskStatuses.Any(t => t.Status == Status))
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
        public IActionResult DeleteTaskStatus(bool Status)
        {
            var taskStatus = _context.TTaskStatuses.FirstOrDefault(t => t.Status == Status);

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
