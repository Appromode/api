using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.Enums
{
    /// <summary>
    /// Type of role access that the user has. This means that multiple roles could have the same general access to areas of the system
    /// More permissions can be addedd through individually adding on links to a specific role instead of through roletypes
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// Basic user
        /// </summary>
        Guest = 0,
        /// <summary>
        /// Standard student user
        /// </summary>
        Student = 1,
        /// <summary>
        /// Elevated user
        /// </summary>
        Lecturer = 2,
        /// <summary>
        /// OP user
        /// </summary>
        Admin = 3
    }
}
