using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Tag data model
    /// User, group, project tags which can differentiate them
    /// </summary>
    [GeneratedController("api/tag")]
    [Table("Tags", Schema = "dbo")]
    public class TagDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Tag id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TagId { get; set; }

        /// <summary>
        /// Name of the tag
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// If a group tag
        /// </summary>
        public bool GroupTag { get; set; } = false;
        /// <summary>
        /// If a project tag
        /// </summary>
        public bool ProjectTag { get; set; } = false;

        /// <summary>
        /// Group id foreign key
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual Int64? GroupId { get; set; }
        /// <summary>
        /// Group project
        /// </summary>
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }

        /// <summary>
        /// Project id foreign key
        /// </summary>
        [ForeignKey("ProjectId")]
        public virtual Int64? ProjectId { get; set; }
        /// <summary>
        /// Project object
        /// </summary>
        [SwaggerExclude]
        public virtual ProjectDM Project { get; set; }

        /// <summary>
        /// User id foreign key
        /// </summary>
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public virtual User User { get; set; }
    }
}
