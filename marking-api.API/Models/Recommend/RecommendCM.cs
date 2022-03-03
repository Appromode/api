using System;
using System.Collections.Generic;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public void GetRecommendUsers(String userId)
        {
            User user = _unitOfWork.Users.GetById(userId);

            var userTags = _unitOfWork.UserTags.Get(
              include: (userTags) => userTags.Include((userTags) => userTags.Tag),
              filter: (userTags) => userTags.UserId == userId
            );

            var tags = userTags.ToList().Select((userTag) => userTag.Tag).ToList();

            tags.ToList().ForEach((item) => Console.WriteLine(item.TagName));

            var similarUsers = _unitOfWork.UserTags.Get(
              include: (userTags) => userTags.Include((userTags) => userTags.Tag).Include((userTags) => userTags.User),
              filter: (userTag) => tags.Contains(userTag.Tag)
            );

            similarUsers.ToList().ForEach((item) => Console.WriteLine(item.User.Email));
        }
    }
}