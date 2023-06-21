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
                var tasks = _context.TTasks.ToList();

                var result = tasks.Select(task => new
                {
                    task.Name,
                    task.Description,
                    task.Email,
                    task.Status

                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Something went wrong");
            }
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

                // Check if the task name already exists in the database
                bool doesTaskExist = _context.TTasks.Any(t => t.Name == task.Name);
                if (doesTaskExist)
                {
                    return Conflict("Task name already exists.");
                }

                _context.TTasks.Add(task);
                _context.SaveChanges();

                return CreatedAtAction("GetTask", new { Name = task.Name }, task);
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





        [HttpGet("{id}")]
        public IActionResult GetTask(string Name)
        {
            var task = _context.TTasks.FirstOrDefault(t => t.Name == Name);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }




        [HttpPut("{id}")]
        public IActionResult UpdateTask(string Name, [FromBody] TTask task)
        {
            if (Name != task.Name)
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
                if (!_context.TTasks.Any(t => t.Name == Name))
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

        [HttpDelete("DeleteTask/{Name}")]
        public IActionResult DeleteTask(string Name)
        {
            var task = _context.TTasks.FirstOrDefault(t => t.Name == Name);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            try
            {
                _context.TTasks.Remove(task);
                _context.SaveChanges();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your needs
                Console.WriteLine(ex);

                return StatusCode(500, "An error occurred while deleting the task"); // 500 Internal Server Error
            }
        }



    }
}
