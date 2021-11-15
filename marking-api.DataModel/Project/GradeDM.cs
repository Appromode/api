using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/grade")]
    [Table("Grades", Schema = "dbo")]
    public class GradeDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GradeId { get; set; }

        public string Comment { get; set; }
        public int Grade { get; set; }

        [ForeignKey("GroupId")]
        public Int64 GroupId { get; set; }
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }

        [ForeignKey("UserId")]
        public Int64 UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set; }

        [ForeignKey("FSFileId")]
        public Int64 FeedbackId { get; set; }
        [SwaggerExclude]
        public virtual FSFileDM Feedback { get; set; }

    }
}
