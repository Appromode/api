using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/usertag")]
    [Table("UserTags", Schema = "dbo")]
    public class UserTagsDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 UserTagId { get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }

        [SwaggerExclude]
        public virtual User User { get; set; }

        [ForeignKey("TagId")]
        public virtual Int64 TagId { get; set; }

        [SwaggerExclude]
        public virtual TagDM Tag { get; set; }
    }
}
