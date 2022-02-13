using marking_api.DataModel.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.Logging
{
    [GeneratedController("api/log")]
    [Table("Logs", Schema = "dbo")]
    public class LogDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Int64 LogId { get; set;}
        public virtual DateTime Date { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Level { get; set; }
        public virtual string Logger { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
    }
}
