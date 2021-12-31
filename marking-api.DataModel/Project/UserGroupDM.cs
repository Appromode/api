using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/usergroup")]
    [Table("UserGroups", Schema = "dbo")]
    public class UserGroupDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserGroupId { get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set; }

        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }
    }
}
