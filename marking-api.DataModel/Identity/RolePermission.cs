using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace marking_api.DataModel.Identity
{
    [Table("IdRolePermission", Schema = "dbo")]
    public class RolePermission : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RolePermissionId { get; set; }

        public PermissionType PermissionType { get; set; }
        public string PermissionSecurity { get; set; }

        [ForeignKey("RoleId")]
        public Int64 RoleId { get; set; }
        [SwaggerExclude]
        public Role Role { get; set; }

        [ForeignKey("SiteAreaId")]
        public Int64 SiteAreaId { get; set; }
        [SwaggerExclude]
        public SiteArea SiteArea { get; set; }
    }
}
