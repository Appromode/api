using marking_api.Data;
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
        
    }

    public class GenericMethodRepository : IGenericMethodRepository
    {
        protected readonly MarkingDbContext _dbContext;

        public GenericMethodRepository(MarkingDbContext dbContext)
        {
            this._dbContext = dbContext;
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
    }
}
