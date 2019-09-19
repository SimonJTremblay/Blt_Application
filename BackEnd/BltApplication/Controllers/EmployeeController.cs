using System.Collections.Generic;
using System.Linq;
using BltData;
using BltData.Models;
using Microsoft.AspNetCore.Mvc;

namespace BltApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employees;
        public EmployeeController(IEmployee employees)
        {
            _employees = employees;
        }

        //GET api/Employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employeesList = _employees.GetAll().ToList();
            return employeesList;
        }

        //GET api/Employee/id
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employees.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
    }
}