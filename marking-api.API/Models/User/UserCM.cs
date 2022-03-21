using System;
using System.Collections.Generic;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using marking_api.DataModel.DTOs;
using marking_api.Global.Extensions;
using System.Linq;

namespace marking_api.API.Models.Recommend
{
    public class UserCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;

        public UserCM(IUnitOfWork unitOfWork)
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
    }
}