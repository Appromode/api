using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Enums
{
    /// <summary>
    /// Type of file permission
    /// Each step up will cover the lower permission
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// No access to the file
        /// </summary>
        None = 0,
        /// <summary>
        /// Can only view and read the file
        /// </summary>
        View = 1,
        /// <summary>
        /// Write to the file
        /// </summary>
        Write = 2,
        /// <summary>
        /// Download the file
        /// </summary>
        Download = 3,
        /// <summary>
        /// Upload a file
        /// </summary>
        Upload = 4,
        /// <summary>
        /// Admin permission. Enables all actions
        /// </summary>
        Admin = 5
    }
}
