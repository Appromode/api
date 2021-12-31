using marking_api.DataModel.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [GeneratedController("api/fsfolderfile")]
    [Table("FSFolderFiles", Schema = "dbo")]
    public class FSFolderFileDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FolderFileId { get; set; }

        [ForeignKey("FileId")]
        public virtual Int64 FileId { get; set; }
        [SwaggerExclude]
        public virtual FSFileDM File { get; set; }

        [ForeignKey("FolderId")]
        public virtual Int64 FolderId { get; set; }
        [SwaggerExclude]
        public virtual FSFolderDM Folder { get; set; }
    }
}
