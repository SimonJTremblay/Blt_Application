using BltApplication.Models;
using BltData;
using BltData.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BltServices
{
    public class ProjectService : IProject
    {
        //private readonly BltContext _context;
        //public ProjectService(BltContext context)
        //{
        //    _context = context;
        //}

        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;
        public ProjectService(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public IEnumerable<Project> GetAll()
        {
            string query =
                @";with scope as 
                    (
	                    SELECT
		                    d.*,
		                    ISNULL((SELECT MAX(value) FROM (VALUES (db.Schedule),(db.Budget), (db.Scope), (db.Issues), (db.OtherRisks)) AS AllValues(value)), 0) AS 'MaxValue'
	                    FROM
			                    Deliverable d
		                    left join
			                    DeliverableBluf db on db.DeliverableId = d.DeliverableId and Date = (SELECT MAX(Date) FROM DeliverableBluf WHERE DeliverableId = d.DeliverableId)
                    )

                    SELECT 
	                    p.ProjectId,
	                    p.Name,
	                    p.Description,
                        p.Owner,
	                    p.Lead,
	                    ISNULL(max(maxValue),0) as 'MaxBluf'
                    FROM 
		                    Project p 
	                    left join ProjectDeliverable pd on pd.ProjectId = p.ProjectId 
	                    left join scope s on s.DeliverableId = pd.DeliverableId
                    GROUP BY
	                    p.ProjectId,
	                    p.Name,
	                    p.Description,
                        p.Owner,
	                    p.Lead
                    ORDER BY
	                    p.ProjectId";
            return db.Query<Project>(query);
        }

        public Project GetById(int projectId)
        {
            return db.Query<Project>("SELECT * FROM Project WHERE ProjectId=@id", new { id = projectId }).SingleOrDefault();
        }

        public void Delete(Project project)
        {
            throw new NotImplementedException();
        }

        public void Add(Project project)
        {
            //_context.Add(project);
            //_context.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<Deliverable> GetDeliverablesFromProject(int projectId)
        {
            //Get all the deliverables from a given project
            const string query =
                @"SELECT
	                d.*
                FROM
		                Deliverable d 
	                JOIN 
		                ProjectDeliverable pd 
			                ON d.DeliverableId = pd.DeliverableId 
	                JOIN
		                Project p
			                ON  p.ProjectId = pd.ProjectId
                WHERE
	                p.ProjectId = @id";

            IEnumerable<Deliverable> ListDeliverables = db.Query<Deliverable>(query, new { id = projectId });

            //Populating List<int> ProjectIdList of each deliverables
            // aka all the projects the deliverable belongs to
            foreach (var item in ListDeliverables)
            {
                string sqlQuery =
                @"SELECT
	                p.ProjectId
                FROM
		                Deliverable d 
	                JOIN 
		                ProjectDeliverable pd 
			                ON d.DeliverableId = pd.DeliverableId 
	                JOIN
		                Project p
			                ON  p.ProjectId = pd.ProjectId
                WHERE
	                d.DeliverableId = @id";

                item.ProjectIdList = db.Query<int>(sqlQuery, new { id = item.DeliverableId }).ToList();
            }
            return ListDeliverables;
        }

        public IEnumerable<Deliverable> GetDeliverablesAndBlufFromProject(int projectId)
        {
            //Call method to return Deliverable + ProjectList
            IEnumerable<Deliverable> ListDeliverables = GetDeliverablesFromProject(projectId);


            // Decided to go with max(blufId) instead of date to prevent if the bluf is
            // edited more than once in a given day.
            string sql =
                @"SELECT
	                * 
                FROM 
	                DeliverableBluf 
                WHERE 
	                    BlufId = (SELECT MAX(BlufId) FROM DeliverableBluf WHERE DeliverableId = @id)
                    AND
		                DeliverableId = @id";

            //Add bluf to each Deliverables
            foreach ( var item in ListDeliverables )
            {
                Bluf bluf = db.Query<Bluf>(sql, new { id = item.DeliverableId }).SingleOrDefault();
                item.Bluf = bluf;
            }

            return ListDeliverables;
        }
    }
}
