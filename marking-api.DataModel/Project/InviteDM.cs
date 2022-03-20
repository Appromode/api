using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;

namespace marking_api.DataModel.Project
{
    [GeneratedController("api/invites")]
    [Table("Invites", Schema = "dbo")]
    public class InviteDM : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 InviteId { get; set; }

        public bool? Status { get; set; }

        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        public virtual GroupDM Group { get; set;}

        [ForeignKey("UserId")]
        public virtual string SenderId { get; set; }
        public virtual User Sender { get; set;}

        [ForeignKey("UserId")]
        public virtual string ReceiverId { get; set; }
        public virtual User Receiver { get; set;}
    }
}
