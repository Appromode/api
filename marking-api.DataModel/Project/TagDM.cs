using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/tag")]
    [Table("Tags", Schema = "dbo")]
    public class TagDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TagId { get; set; }

        public string TagName { get; set; }

        public bool GroupTag { get; set; }
        public bool ProjectTag { get; set; }

        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Int64 ProjectId { get; set; }
        [SwaggerExclude]
        public virtual ProjectDM Project { get; set; }
    }
}
