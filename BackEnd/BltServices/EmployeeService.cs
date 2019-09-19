using BltData;
using BltData.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BltServices
{
    public class EmployeeService : IEmployee
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }
        public IEnumerable<Employee> GetAll()
        {
            return db.Query<Employee>("SELECT * FROM Employees");
        }

        public Employee GetById(int id)
        {
            return db.Query<Employee>("SELECT * FROM Employees WHERE EmployeeId=@Id", new { ID = id }).SingleOrDefault();
        }
    }
}
