using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Logging
{
    /// <summary>
    /// Audit data model
    /// </summary>
    [GeneratedController("api/audit")]
    [Table("Audit", Schema = "dbo")]
    public class AuditDM : BaseDataModel
    {
        /// <summary>
        /// Primary key
        /// Audit id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 AuditId { get; set; }
        /// <summary>
        /// Date of the audit entry
        /// </summary>
        public DateTime AuditDate { get; set; }
        /// <summary>
        /// Type of the audit entry
        /// </summary>
        public string AuditType { get; set; }
        /// <summary>
        /// Name of the table modified
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Key values of the audit entry
        /// </summary>
        public string KeyValues { get; set; }
        /// <summary>
        /// Old values of the entry if the type is modified
        /// </summary>
        public string OldValues { get; set; }
        /// <summary>
        /// New values of the entry if the type is create or modified
        /// </summary>
        public string NewValues { get; set; }
        /// <summary>
        /// Columns that were modified
        /// </summary>
        public string ChangedColumns { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public User User { get; set; }

    }
}
