using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.DataModel.DTOs
{
    /// <summary>
    /// User Data Transfer Object. Transfer the properties that are necessary to make communication with the frontend more secure
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// UUID of the user
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Normalised Username
        /// </summary>
        public string NormalizedUserName { get; set; }
        /// <summary>
        /// Normalised Email
        /// </summary>
        public string NormalizedEmail { get; set; }
        /// <summary>
        /// User firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User profile picture
        /// </summary>
        public byte[] ProfilePicture { get; set; }
        /// <summary>
        /// If user has two factor enabled
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

    }
}
