using Dapper;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Models.Config
{
    public class QueryCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;
        public IConfiguration _config;

        public QueryCM(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public List<dynamic> ExecuteQuery(string sql)
        {
            List<dynamic> rows = null;
            if (!string.IsNullOrWhiteSpace(sql) && !sql.Contains("DELETE") && !sql.Contains("DROP"))
            {
                using (var connection = new SqlConnection(_config.GetConnectionString("DbConnection")))
                {
                    try
                    {
                        rows = connection.Query<dynamic>(sql).ToList();
                    }
                    catch (Exception ex)
                    {
                        rows = new List<dynamic> { ex };
                    }
                }
            }
            return rows;
        }
    }
}
