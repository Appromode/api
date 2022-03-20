using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// File version data model
    /// </summary>
    [GeneratedController("api/fsfileversion")]
    [Table("FSFileVersions", Schema = "dbo")]
    public class FSFileVersionDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Id of the file version
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FileVersionId { get; set; }

        /// <summary>
        /// Data contained within the file
        /// </summary>
        public Byte[] FileData { get; set; }

        /// <summary>
        /// Type of file. e.g. .jpg, .doc, .pdf etc...
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// Type of content such as application/json
        /// </summary>
        public string FilecontentType { get; set; }
        /// <summary>
        /// When the file was uploaded to the system
        /// </summary>
        public DateTime UploadDate { get; set; }
        
        /// <summary>
        /// Name of the file
        /// </summary>
        [MaxLength(255)]
        public string UploadFileName { get; set; }

        /// <summary>
        /// Id of the file
        /// </summary>
        [ForeignKey("FileId")]
        public Int64? FileId { get; set; }
        /// <summary>
        /// File object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFileDM File { get; set; }

        /// <summary>
        /// Id of the file state
        /// </summary>
        [ForeignKey("StateId")]
        public virtual Int64? FileStateId { get; set; }
        /// <summary>
        /// File state object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFileStateDM FileState { get; set; }
    }
}
