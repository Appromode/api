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

        public void AcceptInvite(int inviteId)
        {
            var strategy = _unitOfWork.GenericMethods.CreateExecutionStrategy(); 

            strategy.Execute(() => {
                try {
                    _unitOfWork.GenericMethods.BeginTransaction();

                    InviteDM invite = _unitOfWork.Invites.GetById(inviteId);

                    _unitOfWork.Invites.AddOrUpdate(new InviteDM {
                        Sender = invite.Sender,
                        Receiver = invite.Receiver,
                        GroupId = invite.GroupId,
                        Group = invite.Group,
                        Status = true,
                    });

                    _unitOfWork.Save();

                    _unitOfWork.UserGroups.AddOrUpdate(new UserGroupDM {
                        User = invite.Receiver,
                        Group = invite.Group,
                    });

                    _unitOfWork.Save();

                    _unitOfWork.GenericMethods.CommitTransaction();
                } catch (Exception exception) {
                    Console.WriteLine(exception);
                    _unitOfWork.GenericMethods.RollBackTransaction();
                }
            });
        }
    }
}