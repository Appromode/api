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
    public interface IUserGradeRepository : IGenericModelRepository<UserGradeDM> { }

    public class UserGradeRepository : GenericModelRepository<UserGradeDM>, IUserGradeRepository
    {
        public UserGradeRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
