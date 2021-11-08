using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [Table("FSFolderFiles", Schema = "dbo")]
    public class FSFolderFileDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FSFolderFileId { get; set; }

        [SwaggerExclude]
        [ForeignKey("FSFileId")]
        public Int64 FileId { get; set; }
        public FSFileDM File { get; set; }

        [SwaggerExclude]
        [ForeignKey("FSFolderId")]
        public Int64 FolderId { get; set; }
        public FSFolderDM Folder { get; set; }
    }
}
