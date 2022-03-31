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
    /// Comment data model
    /// </summary>
    [GeneratedController("api/comment")]
    [Table("Comments", Schema = "dbo")]
    public class CommentDM : BaseDataModel
    {
        /// <summary>
        /// Comment id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CommentId { get; set; }

        /// <summary>
        /// Comment content
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Parent thread if foreign key
        /// </summary>
        [ForeignKey("ParentThreadId")]
        public virtual Int64 ParentThreadId { get; set; }
        /// <summary>
        /// Parent thread object
        /// </summary>
        [SwaggerExclude]
        public virtual ThreadDM ParentThread { get; set; }

        /// <summary>
        /// User if foreign key
        /// </summary>
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public virtual User User { get; set;}

        /// <summary>
        /// Quoted comment id foreign key
        /// </summary>
        [ForeignKey("QuotedCommentId")]
        public virtual Int64? QuotedCommentId { get; set; }
        /// <summary>
        /// Quoted comment object
        /// </summary>
        [SwaggerExclude]
        public virtual CommentDM QuotedComment { get; set;}

    }
}
