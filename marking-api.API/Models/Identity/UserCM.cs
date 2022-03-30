using log4net;
using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Models.Identity
{
    /// <summary>
    /// User Controller Model
    /// </summary>
    public class UserCM : BaseModel
    {
        /// <summary>
        /// UnitOfWork database access
        /// </summary>
        public IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserCM(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get list of recommended groups for a user based on the tags that both the user and each group has
        /// Makes use of the dot product of the user and groups to determine which have the most similar tags and which don't
        /// The higher the number the more likely the group is to be a match for the user. 
        /// </summary>
        /// <param name="userId">String</param>
        /// <returns>List of recommended groups in order of dot product</returns>
        public IEnumerable<GroupDM> GetRecommendGroups(string userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var userTags = _unitOfWork.UserTags.Get(
              include: (userTags) => userTags.Include((userTags) => userTags.Tag),
              filter: (userTags) => userTags.UserId == userId
            ).ToList();

            var tags = userTags.ToList().Select((userTag) => userTag.Tag).ToList();

            var results = _unitOfWork.GroupTags.Get(
              include: (table) => (
                table
                  .Include((table) => table.Tag)
                  .Include((table) => table.Group)
              ),
              filter: (table) => tags.Contains(table.Tag)
            )
            .Select((table) => table.Group)
            .GroupBy((table) => table.GroupId)
            .Select((x) => x.First())
            .Distinct();

            return results;
        }

        /// <summary>
        /// Get list of recommded users for a user based on the tags that all the users have.
        /// Makes use of the dot product between the users to see how similar the user's tags are. 
        /// The higher the number the more likely the user is going to be a match and so is higher
        /// on the recommendations list.
        /// </summary>
        /// <param name="userId">String</param>
        /// <returns>List of recommended users in order of dot product</returns>
        public IEnumerable<UserDTO> GetRecommendUsers(string userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var userTags = _unitOfWork.UserTags.Get(
              include: (userTags) => userTags.Include((userTags) => userTags.Tag),
              filter: (userTags) => userTags.UserId == userId
            ).ToList();

            var tags = userTags.ToList().Select((userTag) => userTag.Tag).ToList();

            var results = _unitOfWork.UserTags.Get(
              include: (table) => (
                table
                  .Include((table) => table.Tag)
                  .Include((table) => table.User)
              ),
              filter: (table) => tags.Contains(table.Tag)
            )
              .Where((user) => user.UserId != userId)
              .GroupBy((table) => table.UserId)
              .Select((x) => x.First())
              .Select(x => x.User.ToUserDTO())
              .Distinct();

            return results;
        }

        /// <summary>
        /// Get invites for a user
        /// </summary>
        /// <param name="userId">String</param>
        /// <returns>List of invites the user has recieved</returns>
        public IEnumerable<InviteDM> GetInvites(string userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var invites = _unitOfWork.Invites.Get(
              include: (table) => table.Include((table) => table.Sender)
            ).Where((table) => table.ReceiverId == user.Id);

            return invites;
        }
        
        /// <summary>
        /// Accept group invite method.
        /// Generates a UserGroupDM to add the user to a group
        /// </summary>
        /// <param name="inviteId">Int64 invite id</param>
        /// <returns>UserGroup object with both id of the user and id of the group the user is now part of</returns>
        public UserGroupDM AcceptInvite(Int64 inviteId)
        {
            var strategy = _unitOfWork.GenericMethods.CreateExecutionStrategy();

            Int64 groupId = -1;

            strategy.Execute(() => {
                try
                {
                    _unitOfWork.GenericMethods.BeginTransaction();

                    var invite = _unitOfWork.Invites.GetById(inviteId);

                    invite.Status = true;

                    var updatedInvite = _unitOfWork.Invites.Update(invite);

                    _unitOfWork.Save();

                    var userGroup = new UserGroupDM()
                    {
                        UserId = updatedInvite.ReceiverId,
                        GroupId = updatedInvite.GroupId,
                    };

                    _unitOfWork.UserGroups.AddOrUpdate(userGroup);

                    _unitOfWork.Save();

                    groupId = userGroup.GroupId;

                    _unitOfWork.GenericMethods.CommitTransaction();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    _unitOfWork.GenericMethods.RollBackTransaction();
                }
            });

            return _unitOfWork.UserGroups.GetById(groupId);
        }

        public InviteDM RejectInvite(Int64 inviteId)
        {
            var strategy = _unitOfWork.GenericMethods.CreateExecutionStrategy();

            var invite = _unitOfWork.Invites.GetById(inviteId);

            strategy.Execute(() => {
                try
                {
                    _unitOfWork.GenericMethods.BeginTransaction();

                    invite.Status = false;

                    var updatedInvite = _unitOfWork.Invites.Update(invite);

                    _unitOfWork.Save();

                    _unitOfWork.GenericMethods.CommitTransaction();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    _unitOfWork.GenericMethods.RollBackTransaction();
                }
            });

            return invite;
        }
    }
}
