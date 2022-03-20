using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel
{
    public abstract class BaseDataModel
    {
        public bool canDelete { get; set; }
        public bool deleted { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy {get; set;}
        public DateTime updatedAt { get; set; }
        public string updatedBy { get; set; }
        public DateTime? deletedAt { get; set; }
        public string deletedBy { get; set; }
        public RoleType AccessRole { get; set; } = RoleType.Guest;

        public virtual bool Delete()
        {
            if (canDelete)
            {
                deleted = true;
                deletedAt = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
