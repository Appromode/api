using System;
using System.Collections.Generic;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Project data model
    /// 
    /// </summary>
    [GeneratedController("api/project")]
    [Table("Projects", Schema = "dbo")]
    public class ProjectDM : BaseDataModel
    {
        /// <summary>
        /// Project id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ProjectId { get; set; }

        /// <summary>
        /// Name of project
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Is project is closed to new members
        /// </summary>
        public bool IsClosed { get; set; }
        /// <summary>
        /// If the ethics form has been accepted
        /// </summary>
        public bool EthicsAccepted { get; set; }

        /// <summary>
        /// Ethics form id foreign key
        /// </summary>
        [ForeignKey("FileId")]
        public Int64? EthicsFormId { get; set; }        
        /// <summary>
        /// Ethics form object
        /// </summary>
        [SwaggerExclude]
        public FSFileDM EthicsForm { get; set; }

        /// <summary>
        /// Thread if foreign key
        /// </summary>
        [ForeignKey("LinkedThreadId")]
        public virtual Int64? LinkedThreadId { get; set; }
        /// <summary>
        /// Thread object
        /// </summary>    
        [SwaggerExclude]
        public virtual ThreadDM LinkedThread { get; set; }

        /// <summary>
        /// List of comments
        /// </summary>
        public List<CommentDM> Comments { get; set; }

        /// <summary>
        /// List of project tags
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<TagDM> ProjectTags { get; set; }

        /// <summary>
        /// List of linked threads
        /// </summary>
        [SwaggerExclude]
        [InverseProperty("LinkedProject")]
        public virtual ICollection<ThreadDM> LinkedThreads { get; set; }
    }
}
