using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Extensions
{
    public static class ConversionExtension
    {
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

        public static List<UserDTO> ToUserDTOList(this List<User> users)
        {
            List<UserDTO> userDtos = new List<UserDTO>();
            foreach (User user in users)
            {
                userDtos.Add(user.ToUserDTO());
            }
            return userDtos;
        }

        public static List<User> ToUserList(this List<UserDTO> users)
        {
            List<User> userlist = new List<User>();
            foreach (UserDTO user in users)
            {
                userlist.Add(user.ToUser());
            }
            return userlist;
        }

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

        public static List<LinkDTO> ToLinkDTOList(this ICollection<LinkDM> links)
        {
            List<LinkDTO> linkDtos = new List<LinkDTO>();
            foreach (LinkDM link in links)
            {
                linkDtos.Add(link.ToLinkDTO());
            }
            return linkDtos;
        }
        
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
