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
    [GeneratedController("api/mark")]
    [Table("Marks", Schema = "dbo")]
    public class MarkDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 MarkId { get; set; }
        public int TaskDifficulty { get; set; }
        public int TechnicalAchievements { get; set; }
        public int TechnicalContributions { get; set; }
        public int ProjectContributions { get; set; }
        public int TeamworkSkills { get; set; }
        public int CriticalReflection { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [SwaggerExclude]
        public User User { get; set; }

        [ForeignKey("ProjectId")]
        public Int64 ProjectId { get; set; }
        [SwaggerExclude]
        public ProjectDM Project { get; set; }
    }
}
