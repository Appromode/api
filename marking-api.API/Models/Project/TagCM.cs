using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Models.Project
{
    public class TagCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;
        
        public TagCM(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }

        public void GenerateGroup()
        {

        }
    }
}
