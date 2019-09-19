using BltData;
using BltData.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BltServices
{
    public class TaskService : ITask
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;
        public TaskService(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public void Add(Task task)
        {
            var Params = new DynamicParameters(new
            {
                task.Name,
                task.Status,
                task.TimeEstimation,
                task.DateStart,
                task.DateComplete,
                task.EmployeesAssigned,
            });

            Params.Add("TaskId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            
            //Calling out to the Stored Procedure
            var sql = "AddTasktoDeliverable";

            //should return the Id of the new Task
            db.Query<int>(sql, Params, commandType: CommandType.StoredProcedure);
            int newTaskId = Params.Get<int>("TaskId");

            //populate the DeliverableTasks Table
            var processQuery = "INSERT INTO DeliverableTasks VALUES (@DeliverableId, @TaskId)";
            db.Execute(processQuery, new { task.Deliverable.DeliverableId, @TaskId = newTaskId });
        }

        public void Delete(Task task)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetAll()
        {
            return db.Query<Task>("SELECT * FROM Tasks");
        }

        public Task GetById(int id)
        {
            return db.Query<Task>("SELECT * FROM Tasks WHERE TaskId=@Id", new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<Task> GetTasksFromDeliverable(int deliverableId)
        {
            const string query =
                @" SELECT
	                t.*
                FROM
		                Tasks t
	                JOIN 
		                DeliverableTasks dt 
			                ON dt.TaskId = t.TaskId
	                JOIN
		                Deliverable d
			                ON  d.DeliverableId = dt.DeliverableId
                WHERE
	                d.DeliverableId = @id";

            IEnumerable<Task> result = db.Query<Task>(query, new { id = deliverableId });
            return result;
        }
    }
}
