using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// User grade data model
    /// Pivot table between users and grades
    /// </summary>
    [GeneratedController("api/usergrade")]
    [Table("UserGrades", Schema = "dbo")]
    public class UserGradeDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// User grade id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserGradeId { get; set; }

        /// <summary>
        /// Mark id foreign key
        /// </summary>
        [ForeignKey("MarkId")]
        public Int64 MarkId { get; set; }
        /// <summary>
        /// Mark object
        /// </summary>
        [SwaggerExclude]
        public FeedbackDM Mark { get; set; }

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
