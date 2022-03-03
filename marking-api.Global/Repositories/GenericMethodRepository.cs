using Dapper;
using marking_api.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories
{
    public interface IGenericMethodRepository
    {
        void BeginTransaction();
        void CommitTransaction();        
        void RollBackTransaction();
        IEnumerable<dynamic> ExecuteQuery(string sql);
        
    }

    public class GenericMethodRepository : IGenericMethodRepository
    {
        protected readonly MarkingDbContext _dbContext;
        private readonly IConfiguration _config;

        public GenericMethodRepository(MarkingDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public IEnumerable<dynamic> ExecuteQuery(string sql)
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
