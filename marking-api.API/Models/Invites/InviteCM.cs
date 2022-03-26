using System;
using System.Collections.Generic;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using marking_api.DataModel.DTOs;
using marking_api.Global.Extensions;
using System.Linq;

namespace marking_api.API.Models.Invites
{
    public class InvitesCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;

        public InvitesCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<InviteDM> GetInvites(String userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var invites = _unitOfWork.Invites.Get(
              include: (table) => table.Include((table) => table.Sender)
            ).Where((table) => table.ReceiverId == user.Id);

            return invites;
        }

        public UserGroupDM AcceptInvite(Int64 inviteId)
        {
            var strategy = _unitOfWork.GenericMethods.CreateExecutionStrategy();

            Int64 groupId = -1; 

            strategy.Execute(() => {
                try {
                    _unitOfWork.GenericMethods.BeginTransaction();

                    var invite = _unitOfWork.Invites.GetById(inviteId);

                    invite.Status = true;

                    var updatedInvite = _unitOfWork.Invites.Update(invite);

                    _unitOfWork.Save();

                    var userGroup = new UserGroupDM() {
                        UserId = updatedInvite.ReceiverId,
                        GroupId = updatedInvite.GroupId,
                    };

                    _unitOfWork.UserGroups.AddOrUpdate(userGroup);

                    _unitOfWork.Save();

                    groupId = userGroup.GroupId;

                    _unitOfWork.GenericMethods.CommitTransaction();
                } catch (Exception exception) {
                    Console.WriteLine(exception);
                    _unitOfWork.GenericMethods.RollBackTransaction();
                }
            });

            return _unitOfWork.UserGroups.GetById(groupId);
        }
    }
}