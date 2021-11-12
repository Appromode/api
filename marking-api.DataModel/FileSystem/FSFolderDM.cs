using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [Table("FSFolders", Schema = "dbo")]
    public class FSFolderDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FSFolderId { get; set; }

        [MaxLength(255)]
        public string FolderName { get; set; }

        [MaxLength(255)]
        public string FolderDescription { get; set; }

        public Int64? ParentFolderId { get; set; }
        [SwaggerExclude]
        public virtual FSFolderDM ParentFolder { get; set; }

        [SwaggerExclude]
        public virtual ICollection<FSFolderFileDM> FolderFiles { get; set; }

        [SwaggerExclude]
        public virtual ICollection<FSFolderDM> ChildFolders { get; set; }


    }
}
