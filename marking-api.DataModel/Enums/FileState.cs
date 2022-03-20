using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Enums
{
    /// <summary>
    /// State of a file within the system
    /// </summary>
    public enum FileState
    {
        /// <summary>
        /// None / N/A
        /// </summary>
        None = 0,
        /// <summary>
        /// Draft file to be used as a replacement for the current file
        /// </summary>
        Draft = 1,
        /// <summary>
        /// Currently used file in the system
        /// </summary>
        Current = 2,
        /// <summary>
        /// Previously used file kept for reference
        /// </summary>
        Archived = 3,
        /// <summary>
        /// Deleted file no longer usable / visible within the system
        /// </summary>
        Deleted = 4
    }
}
