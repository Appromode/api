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
    /// <summary>
    /// Base data model which every data model inherits from
    /// </summary>
    public abstract class BaseDataModel
    {
        /// <summary>
        /// If the entry can be deleted
        /// </summary>
        public bool canDelete { get; set; }
        /// <summary>
        /// If the entry has been deleted
        /// </summary>
        public bool deleted { get; set; }
        /// <summary>
        /// The date when the entry was created
        /// </summary>
        public DateTime createdAt { get; set; }
        /// <summary>
        /// Who the entry was created by
        /// </summary>
        public string createdBy {get; set;}
        /// <summary>
        /// When the entry was last updated
        /// </summary>
        public DateTime updatedAt { get; set; }
        /// <summary>
        /// Who the entry was last updated by
        /// </summary>
        public string updatedBy { get; set; }
        /// <summary>
        /// When the entry was deleted
        /// </summary>
        public DateTime? deletedAt { get; set; }
        /// <summary>
        /// Who the entry was deleted by
        /// </summary>
        public string deletedBy { get; set; }
        /// <summary>
        /// What role has access to this entry
        /// </summary>
        public RoleType AccessRole { get; set; } = RoleType.Guest;

        /// <summary>
        /// Method to mark the entry as deleted
        /// </summary>
        /// <returns>Returns true if entry has been marked as deleted</returns>
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
