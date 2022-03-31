using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// Folder role data model
    /// This determines which role has access to what folder
    /// </summary>
    [GeneratedController("api/fsfolderroles")]
    [Table("FSFolderRoles", Schema = "dbo")]
    public class FSFolderRoleDM : BaseDataModel
    {
        /// <summary>
        /// Primary Key
        /// Id of the folder role entity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FolderRoleId { get; set; }

        /// <summary>
        /// Id of the role
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual string RoleId { get; set; }
        /// <summary>
        /// Role Object
        /// </summary>
        [SwaggerExclude]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Id of the file folder
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
