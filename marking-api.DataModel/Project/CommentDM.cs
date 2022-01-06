using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/project")]
    [Table("Comments", Schema = "dbo")]
    public class CommentDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CommentId { get; set; }

        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public Int64 Replies { get; set; }

        [ForeignKey("CommentId")]
        public virtual Int64? ParentCommentId { get; set; }
        [SwaggerExclude]
        public virtual CommentDM ParentComment { get; set; }

        List<CommentDM> ChildComments { get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set;}

        [ForeignKey("ProjectId")]
        public virtual Int64 ProjectId { get; set; }
        [SwaggerExclude]
        public virtual ProjectDM Project { get; set; }
    }
}
