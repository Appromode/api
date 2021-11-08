using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel
{
    public abstract class BaseDataModel
    {
        public bool canDelete { get; set; }
        public bool deleted { get; set; }

        public virtual bool Delete()
        {
            if (canDelete)
            {
                deleted = true;
                return true;
            }
            return false;
        }
    }
}
