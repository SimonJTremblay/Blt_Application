using System.Collections.Generic;
using System.Linq;
using BltData;
using BltData.Models;
using Microsoft.AspNetCore.Mvc;

namespace BltApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _tasks;
        public TaskController(ITask tasks)
        {
            _tasks = tasks;
        }

        //GET api/Task
        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
            var taskList = _tasks.GetAll().ToList();
            return taskList;
        }

        //GET api/Task/id
        [HttpGet("{deliverableId}")]
        public ActionResult<IEnumerable<Task>> Get(int deliverableId)
        {
            var taskList = _tasks.GetTasksFromDeliverable(deliverableId).ToList();

            if (taskList == null)
            {
                return NotFound();
            }

            return taskList;
        }

        // POST: api/Task
        [HttpPost]
        public void Post(Task task)
        {
            _tasks.Add(task);
        }
    }
}