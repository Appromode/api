using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// File data model
    /// </summary>
    [GeneratedController("api/fsfile")]
    [Table("FSFiles", Schema = "dbo")]
    public class FSFileDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Id of the file
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FileId { get; set; }

        /// <summary>
        /// Name of the file
        /// </summary>
        [MaxLength(255)]
        public string FileName { get; set; }

        /// <summary>
        /// Description of the file
        /// </summary>
        [MaxLength(255)]
        public string FileDescription { get; set; }

        /// <summary>
        /// Id of the file folder
        /// </summary>
        [ForeignKey("FolderId")]
        public Int64? FolderID { get; set; }
        /// <summary>
        /// Folder object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFolderDM Folder { get; set; }

        /// <summary>
        /// List of file versions
        /// </summary>
        [SwaggerExclude]
        public ICollection<FSFileVersionDM> FileVersions { get; set; }

        /// <summary>
        /// List of folders this file is in
        /// </summary>
        [SwaggerExclude]
        public ICollection<FSFolderFileDM> FolderFiles { get; set; }
    }
}
