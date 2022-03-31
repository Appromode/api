using marking_api.DataModel.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Logging
{
    /// <summary>
    /// Log data model
    /// </summary>
    [GeneratedController("api/log")]
    [Table("Logs", Schema = "dbo")]
    public class LogDM
    {
        /// <summary>
        /// Primary key
        /// log id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Int64 LogId { get; set;}
        /// <summary>
        /// Date of the log
        /// </summary>
        public virtual DateTime Date { get; set; }
        /// <summary>
        /// Which thread made the log
        /// </summary>
        public virtual string Thread { get; set; }
        /// <summary>
        /// Level of the entry
        /// </summary>
        public virtual string Level { get; set; }
        /// <summary>
        /// What logged the entry
        /// </summary>
        public virtual string Logger { get; set; }
        /// <summary>
        /// Content of the log
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// Error produced
        /// </summary>
        public virtual string Exception { get; set; }
    }
}
