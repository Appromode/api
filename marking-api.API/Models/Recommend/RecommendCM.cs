using System;
using System.Collections.Generic;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
using marking_api.DataModel.DTOs;
using marking_api.DataModel.Identity;
using System.Linq;

namespace marking_api.API.Models.Recommend
{
    public class RecommendCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;

        public RecommendCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDTO> GetRecommendUsers(String userId)
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
              .Select((table) => new UserDTO {
                UserId = table.UserId,
                NormalizedUserName = table.User.NormalizedUserName,
                NormalizedEmail = table.User.NormalizedEmail,
                FirstName = table.User.FirstName,
                LastName = table.User.LastName,
                ProfilePicture = table.User.ProfilePicture,
              })
              .Distinct();

            return results;
        }
    }
}