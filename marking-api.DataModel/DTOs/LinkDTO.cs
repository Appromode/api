using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.DTOs
{
    /// <summary>
    /// Link data transfer object
    /// </summary>
    public class LinkDTO
    {
        /// <summary>
        /// Link Id
        /// </summary>
        public Int64 LinkId { get; set; }
        /// <summary>
        /// Link name
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// Link icon
        /// </summary>
        public string LinkIcon { get; set; }
        /// <summary>
        /// Link click url
        /// </summary>
        public string LinkURL { get; set; }
        /// <summary>
        /// Link position in the menu
        /// </summary>
        public Int64 LinkPosition { get; set; }
        /// <summary>
        /// Parent menu link
        /// </summary>
        public LinkDTO LinkParent { get; set; }
        /// <summary>
        /// List of link children
        /// </summary>
        public virtual ICollection<LinkDTO> LinkChildren { get; set; }
    }
}
