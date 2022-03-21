using System;
using System.Collections.Generic;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using marking_api.Global.Extensions;
using Microsoft.EntityFrameworkCore;
using marking_api.DataModel.DTOs;
using System.Linq;

namespace marking_api.API.Models.Recommend
{
    /// <summary>
    /// Recommend controller model
    /// </summary>
    public class RecommendCM : BaseModel
    {
        /// <summary>
        /// UnitOfWork database access
        /// </summary>
        public IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public RecommendCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get list of recommded users for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserDTO> GetRecommendUsers(String userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var userTags = _unitOfWork.UserTags.Get(
              include: (userTags) => userTags.Include((userTags) => userTags.Tag),
              filter: (userTags) => userTags.UserId == userId
            ).ToList();

            var tags = userTags.ToList().Select((userTag) => userTag.Tag).ToList();

            tags.ForEach((list) => Console.WriteLine(list));

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
              //.Select((table) => new UserDTO {
              //  UserId = table.UserId,
              //  NormalizedUserName = table.User.NormalizedUserName,
              //  NormalizedEmail = table.User.NormalizedEmail,
              //  FirstName = table.User.FirstName,
              //  LastName = table.User.LastName,
              //  ProfilePicture = table.User.ProfilePicture,
              //})
              .Select(x => x.User.ToUserDTO())
              .Distinct();

            return results;
        }
    }
}