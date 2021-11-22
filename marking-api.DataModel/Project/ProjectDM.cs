using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.FileSystem;
using System;
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
        public Int64 EthicsFormId { get; set; }
        [SwaggerExclude]
        public FSFileDM EthicsForm { get; set; }


    }
}
