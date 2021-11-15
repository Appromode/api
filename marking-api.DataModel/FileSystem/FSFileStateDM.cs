using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    [GeneratedController("api/fsfilestate")]
    [Table("FSFileStates", Schema = "dbo")]
    public class FSFileStateDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FileStateId { get; set; }
        
        [MaxLength(255)]
        public string FileStateDescription { get; set; }

        public FileState FileStateType { get; set; }

        [SwaggerExclude]
        public virtual ICollection<FSFileVersionDM> FileVersions { get; set; }
    }
}
