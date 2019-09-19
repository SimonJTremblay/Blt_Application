using BltApplication.Models;
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
    public class DeliverableService : IDeliverable
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;
        public DeliverableService(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public IEnumerable<Deliverable> GetAll()
        {
            return db.Query<Deliverable>("Select * From Deliverable");
        }


        public void Add(Deliverable deliverable)
        {
            var Params = new DynamicParameters(new
            {
                deliverable.Heading,
                deliverable.Priority,
                deliverable.Lead,
                deliverable.DateScheduledStart,
                deliverable.TimeEstimation
            });

            Params.Add("DeliverableId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            //Calling out to the Stored Procedure
            var sql = "AddDeliverableWithProject";

            //should return the Id of the new Deliverable
            db.Query<int>(sql, Params, commandType: CommandType.StoredProcedure);
            int newDeliverableId = Params.Get<int>("DeliverableId");

            //populate the ProjectDeliverable Table
            foreach (var item in deliverable.ProjectIdList)
            {
                var processQuery = "INSERT INTO ProjectDeliverable VALUES (@ProjectId, @DeliverableId)";
                db.Execute(processQuery, new { @DeliverableId = newDeliverableId, @ProjectId = item });
            }
        }

        public void Delete(int deliverableId)
        {
            //before deleteing join tables, I will take the list of tasks associated to the current deliverable
            var sqlTasks = "SELECT t.TaskId from Tasks t JOIN DeliverableTasks dt ON dt.TaskId = t.TaskId JOIN Deliverable d on d.DeliverableId = dt.DeliverableId WHERE d.DeliverableId = @id";

            IEnumerable<int> TaskListToDelete = db.Query<int>(sqlTasks, new { id = deliverableId });

            //To delete a deliverable,we need to delete elements from deliverableTasks join table first.
            var sqlDeliverableTasks = "DELETE FROM DeliverableTasks WHERE DeliverableId = @id";
            db.Execute(sqlDeliverableTasks, new { id = deliverableId });

            //Then, we need to delete elements from the ProjectDeliverable join table
            var sqlProjectTasks = "DELETE FROM ProjectDeliverable WHERE DeliverableId = @id ";
            db.Execute(sqlProjectTasks, new { id = deliverableId });


            //After, we need to delete tasks
            foreach(var task in TaskListToDelete)
            {
                var processExecute = "DELETE FROM Tasks WHERE TaskId = @id ";

                db.Execute(processExecute, new { id = task });
            }

            //Finally, we can delete the deliverable
            var sql = "DELETE FROM Deliverable WHERE DeliverableId = @id ";

            var affectedRows = db.Execute(sql, new { id = deliverableId });

            Console.WriteLine(affectedRows);            
        }
       

        public Deliverable GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}