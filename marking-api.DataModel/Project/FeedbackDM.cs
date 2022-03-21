using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Feedback data model
    /// </summary>
    [GeneratedController("api/mark")]
    [Table("Marks", Schema = "dbo")]
    public class FeedbackDM : BaseDataModel
    {
        /// <summary>
        /// Feedback id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FeedbackId { get; set; }
        /// <summary>
        /// Feedback category task difficulty
        /// </summary>
        public int TaskDifficulty { get; set; }
        /// <summary>
        /// Feedback category technical achievements
        /// </summary>
        public int TechnicalAchievements { get; set; }
        /// <summary>
        /// Feedback category technical contributions
        /// </summary>
        public int TechnicalContributions { get; set; }
        /// <summary>
        /// Feedback category Project contributions
        /// </summary>
        public int ProjectContributions { get; set; }
        /// <summary>
        /// Feedback category teamwork skills
        /// </summary>
        public int TeamworkSkills { get; set; }
        /// <summary>
        /// Feedback category critical reflection
        /// </summary>
        public int CriticalReflection { get; set; }

        /// <summary>
        /// User if foreign key
        /// </summary>
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public User User { get; set; }

        /// <summary>
        /// Project id foreign key
        /// </summary>
        [ForeignKey("ProjectId")]
        public Int64 ProjectId { get; set; }
        /// <summary>
        /// Project object
        /// </summary>
        [SwaggerExclude]
        public ProjectDM Project { get; set; }
    }
}
