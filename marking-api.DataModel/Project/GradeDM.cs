using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Grade data model
    /// </summary>
    [GeneratedController("api/grade")]
    [Table("Grades", Schema = "dbo")]
    public class GradeDM : BaseDataModel
    {
        /// <summary>
        /// Grade id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GradeId { get; set; }

        /// <summary>
        /// Comment on the grade
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Grade
        /// </summary>
        public int Grade { get; set; }

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

        /// <summary>
        /// Feedback id foreign key
        /// </summary>
        [ForeignKey("FeedbackId")]
        public virtual Int64 FeedbackId { get; set; }
        /// <summary>
        /// Feedback object
        /// </summary>
        [SwaggerExclude]
        public virtual FeedbackDM Feedback { get; set; }
    }
}
