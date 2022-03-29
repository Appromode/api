using marking_api.DataModel.FileSystem;
using marking_api.DataModel.Identity;
using marking_api.DataModel.Project;
using marking_api.Global.Repositories;
using marking_api.DataModel.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using marking_api.DataModel.API;
using log4net.Core;

namespace marking_api.API.Models.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class FileCM : BaseModel
    {
        private IUnitOfWork _unitOfWork;
        private ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger</param>
        public FileCM(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Save file method that creates all the necessary files and objects around a file
        /// </summary>
        /// <param name="fileRequest">FileRequest - File + GroupId + UserId</param>
        /// <returns>Saved FSFileDM with versions and folder</returns>
        public Dictionary<FSFileDM, bool> SaveFile(FileRequest fileRequest)
        {
            Dictionary<FSFileDM, bool> result = new Dictionary<FSFileDM, bool>();

            GroupDM group = _unitOfWork.Groups.GetById(fileRequest.GroupId);

            //Save file to generate an id
            FSFileDM fileDM;                        

            //Save file data as a version to be able to create drafts and retire old versions of a file
            //This is where versioning is introduced.
            //Gets existing file if one exists. If not then one is created.
            List<FSFileDM> existingFiles = _unitOfWork.FSFiles.Get(filter: x => x.FileName == fileRequest.File.UploadFileName, include: x => x.Include(y => y.FileVersions).ThenInclude(y => y.FileState)).ToList();

            //If there are no existing files
            if (existingFiles != null)
            {
                //Go through each file version and set to archived
                foreach (var existingFile in existingFiles)
                {
                    if (existingFile.FileVersions.Any(x => x.FileState.FileStateType == FileState.Current))
                    {
                        List<FSFileVersionDM> currentFiles = existingFile.FileVersions.Where(x => x.FileState.FileStateType == FileState.Current).ToList();
                        currentFiles.ForEach(x => x.FileState.FileStateType = FileState.Archived);
                        try
                        {
                            _unitOfWork.FSFileVersions.AddRange(currentFiles);
                            _unitOfWork.Save();
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(null, Level.Error, "Error saving File Version",  ex);
                            result.Add(null, false);
                            return result;
                        }
                    }
                }

                //Should only be one FileDM so if there is an existing one, assign it
                fileDM = existingFiles.FirstOrDefault();
            } else
            {
                //Create new filedm
                fileDM = new FSFileDM()
                {
                    FileName = fileRequest.File.UploadFileName
                };

                try
                {
                    _unitOfWork.FSFiles.Add(fileDM);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.Log(null, Level.Error, "Error saving File", ex);
                    result.Add(null, false);
                    return result;
                }
            }            

            //Create file state. This determines in what state the file is. Current, Draft, Retired.            
            if (fileRequest.File.FileState == null)
            {
                fileRequest.File.FileState = new FSFileStateDM()
                {
                    FileStateType = FileState.Current,
                    FileStateDescription = fileRequest.File.UploadFileName + " " + fileRequest.File.FileVersionId
                };

                try
                {
                    _unitOfWork.FSFileStates.Add(fileRequest.File.FileState);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.Log(null, Level.Error, "Error saving File state", ex);
                    result.Add(null, false);
                    return result;
                }
            }

            fileRequest.File.FileId = fileDM.FileId;
            fileDM.FileVersions = new List<FSFileVersionDM>() { fileRequest.File };

            if (fileRequest.File.FileStateId == 0)
                fileRequest.File.FileStateId = fileRequest.File.FileState.FileStateId;

            //Attach folder for file if one isn't attached
            if (fileDM.Folder == null)
            {
                //Check if a folder exists
                if (_unitOfWork.FSFolders.Get(filter: x => x.FolderName == group.GroupName) != null)
                {
                    FSFolderDM folder = _unitOfWork.FSFolders.Get(filter: x => x.FolderName == group.GroupName).FirstOrDefault();
                    fileDM.FolderID = folder.FolderId;
                    fileDM.Folder = folder;
                }
                else
                {
                    fileDM.Folder = new FSFolderDM()
                    {
                        FolderName = group.GroupName,
                        FolderDescription = group.GroupName + " Files"
                    };

                    try
                    {
                        _unitOfWork.FSFolders.Add(fileDM.Folder);
                        _unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(null, Level.Error, "Error saving Folder", ex);
                        result.Add(null, false);
                        return result;
                    }

                    if (fileDM.FolderID == 0)
                        fileDM.FolderID = fileDM.Folder.FolderId;
                    
                }
                
                //If a folder hasn't been created then there won't be any folderfiles
                //Or there isn't an existing FolderFile for the FileDM
                if ((_unitOfWork.FSFolders.Get(filter: x => x.FolderName == group.GroupName, include: x => x.Include(y => y.FolderFiles)).FirstOrDefault().FolderFiles == null)
                    || _unitOfWork.FSFolderFiles.Get(filter: x => x.FolderId == fileDM.FolderID && x.FileId == fileDM.FileId) == null)
                {
                    fileDM.FolderFiles.Add(new FSFolderFileDM()
                    {
                        FileId = fileDM.FileId,
                        FolderId = fileDM.FolderID.Value
                    });

                    try
                    {
                        _unitOfWork.FSFolderFiles.AddRange(fileDM.FolderFiles);
                        _unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(null, Level.Error, "Error saving Folder file", ex);
                        result.Add(null, false);
                        return result;
                    }
                }

                //If a folder hasn't been created then there won't be any FolderRoleDMs
                if (_unitOfWork.FSFolders.Get(filter: x => x.FolderName == group.GroupName, include: x => x.Include(y => y.FolderRoles)).FirstOrDefault().FolderRoles == null)
                {
                    List<UserRole> userRoles = _unitOfWork.UserRoles.Get(filter: x => x.UserId == fileRequest.UserId).ToList();

                    List<FSFolderRoleDM> folderRoles = new List<FSFolderRoleDM>();

                    foreach (var userrole in userRoles)
                    {
                        FSFolderRoleDM folderRole = new FSFolderRoleDM()
                        {
                            FolderId = fileDM.FolderID.Value,
                            RoleId = userrole.RoleId
                        };

                        _unitOfWork.FSFolderRoles.Add(folderRole);
                        folderRoles.Add(folderRole);
                    }

                    try
                    {
                        _unitOfWork.Save();
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(null, Level.Error, "Error saving Folder role", ex);
                        result.Add(null, false);
                        return result;
                    }
                }
            }                    

            result.Add(fileDM, true);
            return result;
        }
     }
}
