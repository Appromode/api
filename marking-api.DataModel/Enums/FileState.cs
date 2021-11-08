using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Enums
{
    public enum FileState
    {
        None = 0,
        Draft = 1,
        Current = 2,
        Archived = 3,
        Deleted = 4
    }
}
