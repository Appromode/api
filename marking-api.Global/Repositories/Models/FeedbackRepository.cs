using marking_api.Data;
using marking_api.DataModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IFeedbackRepository : IGenericModelRepository<FeedbackDM> { }

    public class FeedbackRepository : GenericModelRepository<FeedbackDM>, IFeedbackRepository
    {
        public FeedbackRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
