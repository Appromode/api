using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Group marker data model
    /// Pivot table between group and users
    /// </summary>
    [GeneratedController("api/groupmarker")]
    [Table("GroupMarkers", Schema = "dbo")]
    public class GroupMarkerDM : BaseDataModel
    {
        /// <summary>
        /// Group marker primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupMarkerId { get; set; }

        /// <summary>
        /// User if foreign key
        /// </summary>
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        /// <summary>
        /// User object
        /// </summary>
        [SwaggerExclude]
        public virtual User User { get; set; }

        /// <summary>
        /// Group id foreign key
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        /// <summary>
        /// Group object
        /// </summary>
        [SwaggerExclude]
        public virtual GroupDM Group { get; set; }
    }
}
