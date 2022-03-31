using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace marking_api.DataModel.FileSystem
{
    /// <summary>
    /// File state data model
    /// </summary>
    [GeneratedController("api/fsfilestate")]
    [Table("FSFileStates", Schema = "dbo")]
    public class FSFileStateDM : BaseDataModel
    {
        /// <summary>
        /// Primary Key
        /// Id of the file state
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 FileStateId { get; set; }
        
        /// <summary>
        /// Description of the file state
        /// </summary>
        [MaxLength(255)]
        public string FileStateDescription { get; set; }

        /// <summary>
        /// Type of the file state e.g. Draft, Current etc...
        /// </summary>
        public FileState FileStateType { get; set; }

        /// <summary>
        /// List of file versions
        /// </summary>
        [SwaggerExclude]
        public virtual ICollection<FSFileVersionDM> FileVersions { get; set; }
    }
}
