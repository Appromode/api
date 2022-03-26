using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using marking_api.DataModel.CustomAttributes;
using marking_api.DataModel.Identity;

namespace marking_api.DataModel.Project
{
    /// <summary>
    /// Invite data model
    /// </summary>
    [GeneratedController("api/invites")]
    [Table("Invites", Schema = "dbo")]
    public class InviteDM : BaseDataModel
    {
        /// <summary>
        /// Invite id primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 InviteId { get; set; }

        /// <summary>
        /// Status of the invite
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// Group id foreign key
        /// </summary>
        [ForeignKey("GroupId")]
        public virtual Int64 GroupId { get; set; }
        /// <summary>
        /// Group object
        /// </summary>
        [SwaggerExclude]
        public virtual GroupDM Group { get; set;}

        /// <summary>
        /// Sender if foreign key
        /// </summary>
        [ForeignKey("SenderId")]
        public virtual string SenderId { get; set; }
        /// <summary>
        /// Sender object
        /// </summary>
        [SwaggerExclude]
        public virtual User Sender { get; set;}

        /// <summary>
        /// Receiver id foreign key
        /// </summary>
        [ForeignKey("ReceiverId")]
        public virtual string ReceiverId { get; set; }
        /// <summary>
        /// Receiver object
        /// </summary>
        [SwaggerExclude]
        public virtual User Receiver { get; set;}
    }
}
