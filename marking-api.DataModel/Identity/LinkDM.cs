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
    [GeneratedController("api/link")]
    [Table("Links", Schema = "dbo")]
    public class LinkDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 LinkId { get; set; }

        public string LinkName { get; set; }
        public string LinkIcon { get; set; }
        public string LinkSecurity { get; set; }
        public string LinkUrl { get; set; }
        public Int64 LinkPosition { get; set; }

        [ForeignKey("LinkId")]
        public virtual Int64? LinkParentId { get; set; }
        [SwaggerExclude]
        public virtual LinkDM LinkParent { get; set; }        

        [SwaggerExclude]
        public virtual ICollection<LinkDM> LinkChildren { get; set; }

        [SwaggerExclude]
        public virtual ICollection<RoleLinkDM> RoleLinks { get; set; }
    }
}
