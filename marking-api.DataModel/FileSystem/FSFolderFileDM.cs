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
        public Int64 FileId { get; set; }
        [SwaggerExclude]
        public FSFileDM File { get; set; }

        [ForeignKey("FolderId")]
        public Int64 FolderId { get; set; }
        [SwaggerExclude]
        public FSFolderDM Folder { get; set; }
    }
}
