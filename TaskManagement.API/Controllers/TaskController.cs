using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TaskManagement.API.Factory;
using TaskManagement.API.Services;
using dbModel = TaskManagement.Domain.Model;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController()
        {
            _taskService= TaskServiceFactory.CreateTaskService();
        }
        // GET: api/<TaskController>
        [HttpGet]
        public IEnumerable<dbModel.Task> GetAllTasks()
        {
            return _taskService.GetAllTasks();

        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public dbModel.Task GetTaskById(int id)
        {
            return _taskService.GetTaskById(id);
        }

        // POST api/<TaskController>
        [HttpPost]
        public IActionResult AddTask(dbModel.Task task)
        {
            try
            {
                _taskService.AddTask(task);
                return Ok(new { message = "Task created successfully." });
            }
            catch (ApplicationException ex)
            {
                return Conflict(new { message = ex.Message }); ;
            }
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateTask(dbModel.Task task)
        {
            try
            {
                _taskService.UpdateTask(task);
                return Ok(new { message = "Task updaated successfully." });
            }
            catch (ApplicationException ex)
            {
                return Conflict(new { message = ex.Message }); ;
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public void DeleteTask(int id)
        {
            _taskService.DeleteTask(id);
        }
    }
}
