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
            return db.Query<Project>("SELECT * FROM Project");
        }

        public Project GetById(int id)
        {
            return db.Query<Project>("SELECT * FROM Project WHERE ProjectId=@ID", new { ID = id }).SingleOrDefault();
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

            IEnumerable<Deliverable> result = db.Query<Deliverable>(query, new { id = projectId });
            return result;
        }
    }
}
