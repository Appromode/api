﻿using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [GeneratedController("api/fsfile")]
    [Table("FSFiles", Schema = "dbo")]
    public class FSFileDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FileId { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(255)]
        public string FileDescription { get; set; }

        [ForeignKey("FolderId")]
        public Int64? FolderID { get; set; }
        [SwaggerExclude]
        public virtual FSFolderDM Folder { get; set; }

        [SwaggerExclude]
        public ICollection<FSFileVersionDM> FileVersions { get; set; }

        [SwaggerExclude]
        public ICollection<FSFolderFileDM> FolderFiles { get; set; }
    }
}
