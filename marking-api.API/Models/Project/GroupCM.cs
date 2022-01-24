using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Models.Project
{
    public class GroupCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;

        public GroupCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GenerateGroup(GroupDM group)
        {
            _unitOfWork.Groups.Add(group);
            //Create group and all other data
            //Return true / false for transaction result
        }
    }
}
