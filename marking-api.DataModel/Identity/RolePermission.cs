using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Role permission data model
    /// </summary>
    [GeneratedController("api/rolepermission")]
    [Table("IdRolePermission", Schema = "dbo")]
    public class RolePermission : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Role permission id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RolePermissionId { get; set; }

        /// <summary>
        /// Type of permission enum
        /// </summary>
        public PermissionType PermissionType { get; set; }
        /// <summary>
        /// What permission is needed
        /// </summary>
        public string PermissionSecurity { get; set; }

        /// <summary>
        /// Foreign key role id
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual string RoleId { get; set; }
        /// <summary>
        /// Role object
        /// </summary>
        [SwaggerExclude]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Site area id
        /// </summary>
        [ForeignKey("SiteAreaId")]
        public virtual Int64 SiteAreaId { get; set; }
        /// <summary>
        /// Site area object
        /// </summary>
        [SwaggerExclude]
        public virtual SiteArea SiteArea { get; set; }
    }
}
