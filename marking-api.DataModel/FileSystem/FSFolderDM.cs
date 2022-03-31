using marking_api.DataModel.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// Folder data model
    /// </summary>
    [GeneratedController("api/fsfolder")]
    [Table("FSFolders", Schema = "dbo")]
    public class FSFolderDM : BaseDataModel
    {
        /// <summary>
        /// Primary Key
        /// Id of the folder
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FolderId { get; set; }

        /// <summary>
        /// Name of the folder
        /// </summary>
        [MaxLength(255)]
        public string FolderName { get; set; }

        /// <summary>
        /// Description of the folder
        /// </summary>
        [MaxLength(255)]
        public string FolderDescription { get; set; }

        /// <summary>
        /// Id of the parent folder
        /// </summary>
        public Int64? ParentFolderId { get; set; }
        /// <summary>
        /// Parent folder object
        /// </summary>
        [SwaggerExclude]
        public virtual FSFolderDM ParentFolder { get; set; }

        /// <summary>
        /// List of files contained within the folder
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<FSFolderFileDM> FolderFiles { get; set; }

        /// <summary>
        /// List of roles that have access to this folder and the files contained within it
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<FSFolderRoleDM> FolderRoles { get; set; }

        /// <summary>
        /// List of child folders
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<FSFolderDM> ChildFolders { get; set; }


    }
}
