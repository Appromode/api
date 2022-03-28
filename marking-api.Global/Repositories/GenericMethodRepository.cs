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
    /// <summary>
    /// Generic method repository interface
    /// </summary>
    public interface IGenericMethodRepository
    {
        /// <summary>
        /// CreateExecutionStrategy
        /// </summary>
        Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy CreateExecutionStrategy();
        /// <summary>
        /// Begin db transaction
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commit db transaction
        /// </summary>
        void CommitTransaction();        
        /// <summary>
        /// Reverse db transaction
        /// </summary>
        void RollBackTransaction();
        /// <summary>
        /// Execute direct db query
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<dynamic> ExecuteQuery(string sql);
        
    }

    /// <summary>
    /// Main generic method repository class
    /// Contains methods that are not specific to one repository / table within the database
    /// </summary>
    public class GenericMethodRepository : IGenericMethodRepository
    {
        private readonly MarkingDbContext _dbContext;
        private readonly IConfiguration _config;

        /// <summary>
        /// GenericMethodRepository constructor
        /// </summary>
        /// <param name="dbContext">MarkingDbContext</param>
        /// <param name="config">IConfiguration - Get configuration strings from appsettings</param>
        public GenericMethodRepository(MarkingDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        /// <summary>
        /// Return execution strategy
        /// </summary>
        public Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy CreateExecutionStrategy() {
            return _dbContext.Database.CreateExecutionStrategy();
        }

        /// <summary>
        /// Begin a transaction in the database so that changes can be rolled back
        /// </summary>
        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Commit transaction once finished
        /// </summary>
        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        /// <summary>
        /// Rollback a started transaction to remove changes
        /// </summary>
        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        /// <summary>
        /// Execute a direct query against the database
        /// Cannot use DELETE or DROP
        /// </summary>
        /// <param name="sql">string - String</param>
        /// <returns>List of dynamic objects</returns>
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
