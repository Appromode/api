using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/group")]
    [Table("Groups", Schema = "dbo")]
    public class GroupDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupId { get; set; }

        public List<UserGroupDM> GroupUsers { get; set; }

        public List<TagDM> Tags { get; set; }

        [Required]
        public string GroupName { get; set; }

        public bool IsClosed { get; set; }
    }
}
