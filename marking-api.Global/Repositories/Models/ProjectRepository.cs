using log4net;
using marking_api.Data;
using marking_api.DataModel.Project;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IProjectRepository : IGenericModelRepository<ProjectDM> { }

    public class ProjectRepository : GenericModelRepository<ProjectDM>, IProjectRepository
    {
        public ProjectRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
