using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/tag")]
    [Table("Tags", Schema = "dbo")]
    public class TagDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TagId { get; set; }

        public string TagName { get; set; }
    }
}
