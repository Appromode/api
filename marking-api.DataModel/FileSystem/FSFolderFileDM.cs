using marking_api.DataModel.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// Folder file data model
    /// Determines which files are in what folder
    /// </summary>
    [GeneratedController("api/fsfolderfile")]
    [Table("FSFolderFiles", Schema = "dbo")]
    public class FSFolderFileDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Id of the folder file entity id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FolderFileId { get; set; }

        /// <summary>
        /// Id of the file
        /// </summary>
        [ForeignKey("FileId")]
        public virtual Int64 FileId { get; set; }
        /// <summary>
        /// File object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFileDM File { get; set; }

        /// <summary>
        /// Id of the folder
        /// </summary>
        [ForeignKey("FolderId")]
        public virtual Int64 FolderId { get; set; }
        /// <summary>
        /// Folder object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFolderDM Folder { get; set; }
    }
}
