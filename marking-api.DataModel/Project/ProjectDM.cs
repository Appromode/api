using System;
using System.Collections.Generic;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/project")]
    [Table("Projects", Schema = "dbo")]
    public class ProjectDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ProjectId { get; set; }

        public string ProjectName { get; set; }
        public bool IsClosed { get; set; }
        public bool EthicsAccepted { get; set; }

        [ForeignKey("FileId")]
        public Int64? EthicsFormId { get; set; }
    
        [SwaggerExclude]
        public FSFileDM EthicsForm { get; set; }

        [ForeignKey("LinkedThreadId")]
        public virtual Int64? LinkedThreadId { get; set; }
    
        [SwaggerExclude]
        public virtual ThreadDM LinkedThread { get; set; }

        public List<CommentDM> Comments { get; set; }

        [SwaggerExclude]
        public virtual ICollection<TagDM> ProjectTags { get; set; }

        [SwaggerExclude]
        [InverseProperty("LinkedProject")]
        public virtual ICollection<ThreadDM> LinkedThreads { get; set; }
    }
}
