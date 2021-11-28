using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.DTOs
{
    public class UserDTO
    {
        string UserId { get; set; }
        string NormalizedUserName { get; set; }
        string NormalizedEmail { get; set; }
        byte[] ProfilePicture { get; set; }
        bool TwoFactorEnabled { get; set; }

    }
}
