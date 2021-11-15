using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [GeneratedController("api/fsfileversion")]
    [Table("FSFileVersions", Schema = "dbo")]
    public class FSFileVersionDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FSFileVersionId { get; set; }

        public Byte[] FileData { get; set; }

        public string FileExtension { get; set; }
        public string FilecontentType { get; set; }
        public DateTime UploadDate { get; set; }

        [MaxLength(255)]
        public string UploadFileName { get; set; }

        [ForeignKey("FSFileId")]
        public Int64? FileId { get; set; }
        [SwaggerExclude]
        public virtual FSFileDM File { get; set; }

        [ForeignKey("FSStateId")]
        public Int64? FileStateId { get; set; }
        [SwaggerExclude]
        public FSFileStateDM FileState { get; set; }
    }
}
