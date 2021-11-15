using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Identity
{
    [GeneratedController("api/sitearea")]
    [Table("IdSiteArea", Schema = "dbo")]
    public class SiteArea : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 SiteAreaId { get; set; }

        public string Link { get; set; }
    }
}
