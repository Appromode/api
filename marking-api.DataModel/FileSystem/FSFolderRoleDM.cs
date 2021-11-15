using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [GeneratedController("api/fsfolderroles")]
    [Table("FSFolderRoles", Schema = "dbo")]
    public class FSFolderRoleDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FolderRoleId { get; set; }

        [ForeignKey("RoleId")]
        public Int64 RoleId { get; set; }
        [SwaggerExclude]
        public Role Role { get; set; }

        [ForeignKey("FSFoleId")]
        public Int64 FolderId { get; set; }
        [SwaggerExclude]
        public FSFolderDM Folder { get; set; }
    }
}
