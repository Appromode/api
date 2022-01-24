﻿using marking_api.Global.Repositories;
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

        public void GenerateGroup()
        {
            //Create group and all other data
            //Return true / false for transaction result
        }
    }
}
