using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Enums
{
    /// <summary>
    /// Type of audit
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// None / N/A
        /// </summary>
        None = 0,
        /// <summary>
        /// Create audit. Created an entry in the database
        /// </summary>
        Create = 1,
        /// <summary>
        /// Update an entry within the database
        /// </summary>
        Update = 2,
        /// <summary>
        /// Deleted an entry within the database
        /// </summary>
        Delete = 3
    }
}
