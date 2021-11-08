using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [Table("FSFiles", Schema = "dbo")]
    public class FSFileDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FSFileId { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(255)]
        public string FileDescription { get; set; }

        [SwaggerExclude]
        [ForeignKey("FSFolderId")]
        public Int64? FolderID { get; set; }
        public virtual FSFolderDM Folder { get; set; }

        [SwaggerExclude]
        public ICollection<FSFileVersionDM> FileVersions { get; set; }

        [SwaggerExclude]
        public ICollection<FSFolderFileDM> FolderFiles { get; set; }
    }
}
