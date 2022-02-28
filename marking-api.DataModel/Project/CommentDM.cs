using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/comment")]
    [Table("Comments", Schema = "dbo")]
    public class CommentDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CommentId { get; set; }

        public string CommentText { get; set; }

        [ForeignKey("ParentThreadId")]
        public virtual Int64 ParentThreadId { get; set; }
        [SwaggerExclude]
        public virtual ThreadDM ParentThread { get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set;}

        [ForeignKey("QuotedCommentId")]
        public virtual Int64 QuotedCommentId { get; set; }
        [SwaggerExclude]
        public virtual CommentDM QuotedComment { get; set;}

    }
}
