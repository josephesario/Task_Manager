using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Models;


namespace Task_Manager.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly taskManagerDbContext _context;

        public TaskController(taskManagerDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTask")]
        public IActionResult GetAllTask()
        {
            try
            {
                var tasks = _context.TTasks.Include(t => t.Status).Include(t => t.EmailNavigation).ToList();

                // Extract foreign key names
                var statusForeignKeyName = _context.Model.FindEntityType(typeof(TTask)).FindNavigation(nameof(TTask.Status)).ForeignKey.Properties.Single().Name;
                var emailForeignKeyName = _context.Model.FindEntityType(typeof(TTask)).FindNavigation(nameof(TTask.EmailNavigation)).ForeignKey.Properties.Single().Name;

                var result = tasks.Select(task => new
                {
                    task.Id,
                    task.Name,
                    task.Description,
                    Status = new
                    {
                        task.Status.StatusId,
                        task.Status.Status,
                        task.Status.CreatedOn
                    },
                    Email = new
                    {
                        task.EmailNavigation.Email,
                        task.EmailNavigation.Password,
                        task.EmailNavigation.CreatedOn
                    }
                }).ToList();

                return Ok(new { Tasks = result, StatusForeignKeyName = statusForeignKeyName, EmailForeignKeyName = emailForeignKeyName });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Something went wrong");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _context.TTasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }


        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] TTask task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.TTasks.Add(task);
                _context.SaveChanges();

                return CreatedAtAction("GetTask", new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                // Handle specific exception types and return corresponding responses
                if (ex is DbUpdateException)
                {
                    return StatusCode(500, "Error saving the task to the database.");
                }
                else
                {
                    // Return a generic error message for other exceptions
                    return StatusCode(500, "An error occurred while processing the request.");
                }
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TTasks.Any(t => t.Id == id))
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
        public IActionResult DeleteTask(int id)
        {
            var task = _context.TTasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            _context.TTasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
