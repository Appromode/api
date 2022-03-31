using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Menu link data model
    /// </summary>
    [GeneratedController("api/link")]
    [Table("Links", Schema = "dbo")]
    public class LinkDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Link id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 LinkId { get; set; }

        /// <summary>
        /// Name of the link
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// Link icon
        /// </summary>
        public string LinkIcon { get; set; }
        /// <summary>
        /// Link security string to limit what a user has access to
        /// </summary>
        public string LinkSecurity { get; set; }
        /// <summary>
        /// Url which the link directs to
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// Position within the menu of the link
        /// </summary>
        public Int64 LinkPosition { get; set; }

        /// <summary>
        /// Parent link id
        /// </summary>
        [ForeignKey("LinkId")]
        public virtual Int64? LinkParentId { get; set; }
        /// <summary>
        /// Parent link object
        /// </summary>
        [SwaggerExclude]
        public virtual LinkDM LinkParent { get; set; }        

        /// <summary>
        /// List of link children
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<LinkDM> LinkChildren { get; set; }

        /// <summary>
        /// List of role links
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<RoleLinkDM> RoleLinks { get; set; }
    }
}
