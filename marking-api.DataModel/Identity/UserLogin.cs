using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Identity
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public UserLogin() : base() { }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
