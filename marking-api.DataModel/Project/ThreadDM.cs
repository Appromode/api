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
    [GeneratedController("api/thread")]
    [Table("Threads", Schema = "dbo")]
    public class ThreadDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ThreadId { get; set; }

        public string ThreadTitle { get; set; }

        public string ThreadDesc { get; set; }

        public bool ThreadStatus { get; set; }

        public int Replies {get; set; }

        [ForeignKey("UserId")]
        public virtual string UserId { get; set;}
        [SwaggerExclude]
        public virtual User User { get; set;}

        [ForeignKey("LinkedProjectId")]
        public virtual Int64? LinkedProjectId { get; set;}
        [SwaggerExclude]
        public virtual ProjectDM LinkedProject { get; set;}

        public List<CommentDM> Comments { get; set; }

        public int? TotalMembers { get; set; }
        
        [SwaggerExclude]
        [InverseProperty("LinkedThread")]
        public virtual ICollection<ProjectDM> LinkedProjects { get; set; }

    }
}
