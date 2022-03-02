using marking_api.Data;
using marking_api.DataModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IThreadRepository : IGenericModelRepository<ThreadDM> { }

    public class ThreadRepository : GenericModelRepository<ThreadDM>, IThreadRepository
    {
        public ThreadRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
