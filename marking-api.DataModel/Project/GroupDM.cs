using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Project
{
    [Table("Groups", Schema = "dbo")]
    public class GroupDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupId { get; set; }

        public string GroupName { get; set; }
        public bool IsClosed { get; set; }
    }
}
