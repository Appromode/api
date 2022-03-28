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
    /// Thread data model
    /// Over arching class for posts on the forum
    /// </summary>
    [GeneratedController("api/thread")]
    [Table("Threads", Schema = "dbo")]
    public class ThreadDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Thread id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ThreadId { get; set; }

        /// <summary>
        /// Title of the thread
        /// </summary>
        public string ThreadTitle { get; set; }

        /// <summary>
        /// Content of the thread
        /// </summary>
        public string ThreadContent { get; set; }

        /// <summary>
        /// Status of the thread
        /// </summary>
        public bool ThreadStatus { get; set; }

        /// <summary>
        /// Number of replies to the thread
        /// </summary>
        public int ReplyCount {get; set; }

        /// <summary>
        /// User id foreign key
        /// </summary>
        [ForeignKey("UserId")]
        public virtual string UserId { get; set;}
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public virtual User User { get; set;}

        /// <summary>
        /// Project id foreign key
        /// </summary>
        [ForeignKey("LinkedProjectId")]
        public virtual Int64? LinkedProjectId { get; set;}
        /// <summary>
        /// Project object
        /// </summary>
        [SwaggerExclude]
        public virtual ProjectDM LinkedProject { get; set;}

        /// <summary>
        /// 
        /// </summary>
        public int? TotalMembers { get; set; }
        
        /// <summary>
        /// List of linked projects
        /// </summary>
        [SwaggerExclude]
        [InverseProperty("LinkedThread")]
        public virtual ICollection<ProjectDM> LinkedProjects { get; set; }

        /// <summary>
        /// List of comments
        /// </summary>
        [SwaggerExclude]
        public ICollection<CommentDM> Comments { get; set; }

    }
}
