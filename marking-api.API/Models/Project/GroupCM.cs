using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Models.Project
{
    /// <summary>
    /// Group controller model
    /// </summary>
    public class GroupCM : BaseModel
    {
        /// <summary>
        /// UnitOfWork database access
        /// </summary>
        public IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork</param>
        public GroupCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Generates a group and saves within the database
        /// </summary>
        /// <param name="group">Base elements of the group to save and flesh out</param>
        public void GenerateGroup(GroupDM group)
        {
            _unitOfWork.Groups.Add(group);
            //Create group and all other data
            //Return true / false for transaction result
        }
    }
}
