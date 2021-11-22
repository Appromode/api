using marking_api.Data;
using marking_api.DataModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface ITagRepository : IGenericModelRepository<TagDM> { }

    public class TagRepository : GenericModelRepository<TagDM>, ITagRepository
    {
        public TagRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
