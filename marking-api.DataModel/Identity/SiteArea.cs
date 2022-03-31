using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    /// <summary>
    /// Site area data model
    /// Used for determining access to areas of the system
    /// </summary>
    [GeneratedController("api/sitearea")]
    [Table("IdSiteArea", Schema = "dbo")]
    public class SiteArea : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Site area id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 SiteAreaId { get; set; }

        /// <summary>
        /// Url link of the page
        /// </summary>
        public string Link { get; set; }
    }
}
