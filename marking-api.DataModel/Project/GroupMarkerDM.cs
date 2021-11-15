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
    [GeneratedController("api/groupmarker")]
    [Table("GroupMarkers", Schema = "dbo")]
    public class GroupMarkerDM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GroupMarkerId { get; set; }

        [ForeignKey("UserId")]
        public Int64 UserId { get; set; }
        [SwaggerExclude]
        public virtual User User { get; set; }
    }
}
