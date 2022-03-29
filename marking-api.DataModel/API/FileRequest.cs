using marking_api.DataModel.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.API
{
    /// <summary>
    /// File Request to save a version of a file. If no version exists then everything will be scaffolded
    /// </summary>
    public class FileRequest
    {
        /// <summary>
        /// File containing data
        /// </summary>
        public FSFileVersionDM File { get; set; }

        /// <summary>
        /// Id of the group that the user belongs to
        /// </summary>
        public Int64 GroupId { get; set; }

        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; set; }
    }
}
