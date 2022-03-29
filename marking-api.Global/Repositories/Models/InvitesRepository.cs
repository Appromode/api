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
    public interface IInvitesRepository : IGenericModelRepository<InviteDM> { }

    public class InvitesRepository : GenericModelRepository<InviteDM>, IInvitesRepository
    {
        public InvitesRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
