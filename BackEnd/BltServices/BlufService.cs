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
            return db.Query<Bluf>("SELECT * FROM Blufs WHERE BlufId=@Id", new { Id = id }).SingleOrDefault();
        }

        public void UpdateBluf(Bluf bluf)
        {
            //By design, the update functions add a new row to the database, therefore not deleting the previous data.

            DateTime date = DateTime.Now;

            var sql = @"INSERT INTO[dbo].[DeliverableBluf]
                        ([DeliverableId]
                          ,[Schedule]
                          ,[Budget]
                          ,[Scope]
                          ,[OtherRisks]
                          ,[Issues]
                          ,[Date])
                       VALUES
                            @DeliverableId,
                            @Schedule,
                            @Budget,
                            @Scope,
                            @OtherRisks,
                            @Issues,
                            @Date";

            var isSuccess = db.Execute(sql,
                new
                {
                    @DeliverableId = bluf.DeliverableId,
                    @Schedule = bluf.Schedule ,
                    @Budget = bluf.Budget,
                    @Scope = bluf.Scope ,
                    @OtherRisks = bluf.OtherRisks,
                    @Issues = bluf.Issues,
                    @Date = date,
                }
            );
        }
    }
}
