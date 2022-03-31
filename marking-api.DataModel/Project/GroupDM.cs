using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Group data model
    /// </summary>
    [GeneratedController("api/group")]
    [Table("Groups", Schema = "dbo")]
    public class GroupDM : BaseDataModel
    {
        /// <summary>
        /// Group id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupId { get; set; }

        /// <summary>
        /// List of group users
        /// </summary>
        public List<UserGroupDM> GroupUsers { get; set; }

        /// <summary>
        /// List of group tags
        /// </summary>
        [SwaggerExclude]
        public List<TagDM> GroupTags { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        /// <summary>
        /// If group is closed to new members
        /// </summary>
        public bool IsClosed { get; set; }
    }
}
