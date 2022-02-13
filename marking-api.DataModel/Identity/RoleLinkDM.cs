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
    [GeneratedController("api/rolelink")]
    [Table("RoleLinks", Schema = "dbo")]
    public class RoleLinkDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RoleLinkId { get; set; }

        [ForeignKey("RoleId")]
        public virtual string RoleId { get; set; }
        [SwaggerExclude]
        public virtual Role Role { get; set; }

        [ForeignKey("LinkId")]
        public virtual Int64 LinkId { get; set; }
        [SwaggerExclude]
        public virtual LinkDM Link { get; set; }
    }
}
