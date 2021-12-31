using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/rolepermission")]
    [Table("IdRolePermission", Schema = "dbo")]
    public class RolePermission : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RolePermissionId { get; set; }

        public PermissionType PermissionType { get; set; }
        public string PermissionSecurity { get; set; }

        [ForeignKey("RoleId")]
        public virtual string RoleId { get; set; }
        [SwaggerExclude]
        public virtual Role Role { get; set; }

        [ForeignKey("SiteAreaId")]
        public virtual Int64 SiteAreaId { get; set; }
        [SwaggerExclude]
        public virtual SiteArea SiteArea { get; set; }
    }
}
