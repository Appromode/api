using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Extensions
{
    /// <summary>
    /// Object conversion extension methods
    /// </summary>
    public static class ConversionExtension
    {
        /// <summary>
        /// Convert User object to userdto object
        /// </summary>
        /// <param name="user">Extended User object</param>
        /// <returns></returns>
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                ProfilePicture = user.ProfilePicture,
                UserId = user.Id
            };
        }

        /// <summary>
        /// Convert UserDTO object to User object
        /// </summary>
        /// <param name="user">Extended UserDTO object</param>
        /// <returns>Converted user object</returns>
        public static User ToUser(this UserDTO user)
        {
            return new User
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
                ProfilePicture = user.ProfilePicture,
            };
        }

        /// <summary>
        /// Converts list of user objects to userdto objects
        /// </summary>
        /// <param name="users">Extended list of user objects</param>
        /// <returns>Converted list of userdto objects</returns>
        public static List<UserDTO> ToUserDTOList(this List<User> users)
        {
            List<UserDTO> userDtos = new List<UserDTO>();
            foreach (User user in users)
            {
                userDtos.Add(user.ToUserDTO());
            }
            return userDtos;
        }

        /// <summary>
        /// Converts list of UserDTO objects to list of User objects
        /// </summary>
        /// <param name="users">Extended list of UserDTO objects</param>
        /// <returns>Converted list of user objects</returns>
        public static List<User> ToUserList(this List<UserDTO> users)
        {
            List<User> userlist = new List<User>();
            foreach (UserDTO user in users)
            {
                userlist.Add(user.ToUser());
            }
            return userlist;
        }

        /// <summary>
        /// Convert LinkDM object to a linkDTO object
        /// </summary>
        /// <param name="link">Extended LinkDM object</param>
        /// <returns>Converted LinkDTO object</returns>
        public static LinkDTO ToLinkDTO(this LinkDM link)
        {
            return new LinkDTO
            {
                LinkId = link.LinkId,
                LinkName = link.LinkName,
                LinkPosition = link.LinkPosition,
                LinkIcon = link.LinkIcon,
                LinkURL = link.LinkUrl,
                LinkParent = (link.LinkParent == null ? null : link.LinkParent.ToLinkDTO()),
                LinkChildren = (link.LinkChildren == null ? null : link.LinkChildren.ToLinkDTOList())                
            };
        }

        /// <summary>
        /// Convert LinkDTO object to a linkDM object
        /// </summary>
        /// <param name="link">Extended LinkDTO object</param>
        /// <returns>Converted LinkDM object</returns>
        public static LinkDM ToLinkDM(this LinkDTO link)
        {
            return new LinkDM
            {
                LinkId = link.LinkId,
                LinkName = link.LinkName,
                LinkUrl = link.LinkURL,
                LinkIcon = link.LinkIcon,
                LinkPosition = link.LinkPosition,
                LinkParent = link.LinkParent.ToLinkDM(),
                LinkChildren = (link.LinkChildren == null ? null : link.LinkChildren.ToLinkDMList()),
            };
        }

        /// <summary>
        /// Convert a list of LinkDM objects to LinkDTO objects
        /// </summary>
        /// <param name="links">Extended list of LinkDM objects</param>
        /// <returns>List of converted LinkDTO objects</returns>
        public static List<LinkDTO> ToLinkDTOList(this ICollection<LinkDM> links)
        {
            List<LinkDTO> linkDtos = new List<LinkDTO>();
            foreach (LinkDM link in links)
            {
                linkDtos.Add(link.ToLinkDTO());
            }
            return linkDtos;
        }
        
        /// <summary>
        /// Convert a list of LinkDTO objects to LinkDM objects
        /// </summary>
        /// <param name="links">Extended list of linkDTO objects</param>
        /// <returns>List of converted LinkDM objects</returns>
        public static List<LinkDM> ToLinkDMList(this ICollection<LinkDTO> links)
        {
            List<LinkDM> linkdms = new List<LinkDM>();
            foreach (LinkDTO link in links)
            {
                linkdms.Add(link.ToLinkDM());
            }
            return linkdms;
        }
    }
}
