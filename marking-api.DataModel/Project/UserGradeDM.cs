using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/usergrade")]
    [Table("UserGrades", Schema = "dbo")]
    public class UserGradeDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserGradeId { get; set; }

        public int Grade { get; set; }

        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set; }
    }
}
