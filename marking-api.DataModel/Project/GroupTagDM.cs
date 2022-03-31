using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// User tag data model
    /// Pivot table between groups and tags.
    /// </summary>
    [GeneratedController("api/grouptag")]
    [Table("GroupTags", Schema = "dbo")]
    public class GroupTagDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Group tag id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupTagId { get; set; }

        /// <summary>
        /// Group id foreign key
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        /// <summary>
        /// Group object
        /// </summary>
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }

        /// <summary>
        /// Tag id foreign key
        /// </summary>
        [ForeignKey("TagId")]
        public virtual Int64 TagId { get; set; }
        /// <summary>
        /// Tag object
        /// </summary>
        [SwaggerExclude]
        public virtual TagDM Tag { get; set; }
    }
}
