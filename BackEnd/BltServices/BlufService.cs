using BltData;
using BltData.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BltServices
{
    public class BlufService : IBluf
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection db;
        public BlufService(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public Bluf GetById(int id)
        {
            string sql = 
                @"SELECT
	                * 
                FROM 
	                DeliverableBluf 
                WHERE 
	                    Date = (SELECT MAX(Date) FROM DeliverableBluf WHERE DeliverableId = @Id)
                    AND
		                DeliverableId = @Id";

            return db.Query<Bluf>(sql, new { Id = id }).SingleOrDefault();
        }

        public void PostBluf(Bluf bluf)
        {
            //By design, the update functions add a new row to the database, therefore not deleting the previous data.

            var sql = @"INSERT INTO[dbo].[DeliverableBluf]
                        ([DeliverableId]
                          ,[Schedule]
                          ,[Budget]
                          ,[Scope]
                          ,[Issues]
                          ,[OtherRisks]
                          ,[Date])
                       VALUES
                            @DeliverableId,
                            @Schedule,
                            @Budget,
                            @Scope,
                            @Issues,
                            @OtherRisks,
                            @Date";

            var isSuccess = db.Execute(sql,
                new
                {
                    @DeliverableId = bluf.DeliverableId,
                    @Schedule = bluf.Schedule ,
                    @Budget = bluf.Budget,
                    @Scope = bluf.Scope ,
                    @Issues = bluf.Issues,
                    @OtherRisks = bluf.OtherRisks,
                    @Date = bluf.Date,
                }
            );
        }
    }
}
