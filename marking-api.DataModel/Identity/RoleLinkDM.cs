using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Role link data model
    /// </summary>
    [GeneratedController("api/rolelink")]
    [Table("RoleLinks", Schema = "dbo")]
    public class RoleLinkDM
    {
        /// <summary>
        /// Primary key
        /// Role link id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RoleLinkId { get; set; }

        /// <summary>
        /// Role id
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual string RoleId { get; set; }
        /// <summary>
        /// Role object
        /// </summary>
        [SwaggerExclude]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Link id
        /// </summary>
        [ForeignKey("LinkId")]
        public virtual Int64 LinkId { get; set; }
        /// <summary>
        /// Link object
        /// </summary>
        [SwaggerExclude]
        public virtual LinkDM Link { get; set; }
    }
}
