using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Logging
{
    [GeneratedController("api/audit")]
    [Table("Audit", Schema = "dbo")]
    public class AuditDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 AuditId { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditType { get; set; }
        public string TableName { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string ChangedColumns { get; set; }

        [ForeignKey("UserId")]
        [SwaggerExclude]
        public virtual string UserId { get; set; }
        public virtual User User { get; set; }

    }
}
