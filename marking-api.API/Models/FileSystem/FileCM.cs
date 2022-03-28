using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.API.Models.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class FileCM : BaseModel
    {
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public FileCM(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Save file method that creates all the necessary files and objects around a file
        /// </summary>
        /// <param name="file">FSFileDM</param>
        /// <param name="groupId">Int64</param>
        /// <param name="userId">String</param>
        /// <returns>Saved FSFileDM with versions and folder</returns>
        public FSFileDM SaveFile(FSFileVersionDM file, long groupId, string userId)
        {
            FSFileDM fileDM = new FSFileDM()
            {

            };

            FSFileStateDM fileState = new FSFileStateDM()
            {

            };

            FSFolderDM folder = new FSFolderDM()
            {

            };

            List<UserRole> userRoles = _unitOfWork.UserRoles.Get(filter: x => x.UserId == userId ).ToList();

            return null;
        }
     }
}
