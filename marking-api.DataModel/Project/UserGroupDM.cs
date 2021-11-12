using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [Table("UserGroups", Schema = "dbo")]
    public class UserGroupDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserGroupId { get; set; }

        [ForeignKey("UserId")]
        public Int64 UserId { get; set; }
        [SwaggerExclude]
        public User User { get; set; }

        [ForeignKey("GroupId")]
        public Int64 GroupId { get; set; }
        [SwaggerExclude]
        public GroupDM Group { get; set; }
    }
}
