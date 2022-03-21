using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// User tag data model
    /// Pivot table between users and tags.
    /// </summary>
    [GeneratedController("api/usertag")]
    [Table("UserTags", Schema = "dbo")]
    public class UserTagsDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// user tag id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserTagId { get; set; }

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
