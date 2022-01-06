using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //Models
        IFSFileRepository FSFiles { get; }
        IFSFileStateRepository FSFileStates { get; }
        IFSFileVersionRepository FSFileVersions { get; }
        IFSFolderFileRepository FSFolderFiles { get; }
        IFSFolderRepository FSFolders { get; }
        IFSFolderRoleRepository FSFolderRoles { get; }
        ICommentRepository Comments { get; }
        IGradeRepository Grades { get; }
        IGroupMarkerRepository GroupMarkers { get; }
        IGroupRepository Groups { get; }
        IProjectRepository Projects { get; }
        IRoleClaimRepository RoleClaims { get; }
        IRoleRepository Roles { get; }
        IRolePermissionRepository RolePermissions { get; }
        ISiteAreaRepository SiteAreas { get; }
        ITagRepository Tags { get; }
        IUserClaimRepository UserClaims { get; }
        IUserGradeRepository UserGrades { get; }
        IUserGroupRepository UserGroups { get; }
        IUserLoginRepository UserLogins { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IUserTokenRepository UserTokens { get; }
        
        //Views

        
        int Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarkingDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;

        public UnitOfWork(MarkingDbContext dbContext, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;

            //Models
            FSFiles = new FSFileRepository(dbContext);
            FSFileStates = new FSFileStateRepository(dbContext);
            FSFileVersions = new FSFileVersionRepository(dbContext);
            FSFolderFiles = new FSFolderFileRepository(dbContext);
            FSFolders = new FSFolderRepository(dbContext);
            FSFolderRoles = new FSFolderRoleRepository(dbContext);
            Comments = new CommentRepository(dbContext);
            Grades = new GradeRepository(dbContext);
            GroupMarkers = new GroupMarkerRepository(dbContext);
            Groups = new GroupRepository(dbContext);
            Projects = new ProjectRepository(dbContext);
            RoleClaims = new RoleClaimRepository(dbContext);
            RolePermissions = new RolePermissionRepository(dbContext);
            Roles = new RoleRepository(dbContext);
            SiteAreas = new SiteAreaRepository(dbContext);
            Tags = new TagRepository(dbContext);
            UserClaims = new UserClaimRepository(dbContext);
            UserGrades = new UserGradeRepository(dbContext);
            UserGroups = new UserGroupRepository(dbContext);
            UserLogins = new UserLoginRepository(dbContext);
            Users = new UserRepository(dbContext);
            UserRoles = new UserRoleRepository(dbContext);
            UserTokens = new UserTokenRepository(dbContext);

            //Views
        }

        //Models
        public IFSFileRepository FSFiles { get; private set; }
        public IFSFileStateRepository FSFileStates { get; private set; }
        public IFSFileVersionRepository FSFileVersions { get; private set; }
        public IFSFolderFileRepository FSFolderFiles { get; private set; }
        public IFSFolderRepository FSFolders { get; private set; }
        public IFSFolderRoleRepository FSFolderRoles { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IGradeRepository Grades { get; private set; }
        public IGroupMarkerRepository GroupMarkers { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IRoleClaimRepository RoleClaims { get; private set; }
        public IRolePermissionRepository RolePermissions { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ISiteAreaRepository SiteAreas { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IUserClaimRepository UserClaims { get; private set; }
        public IUserGradeRepository UserGrades { get; private set; }
        public IUserGroupRepository UserGroups { get; private set; }
        public IUserLoginRepository UserLogins { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserRoleRepository UserRoles { get; private set; }
        public IUserTokenRepository UserTokens { get; private set; }

        //Views

        public int Save() => _dbContext.SaveChanges();
        public void Dispose() => _dbContext.Dispose();
    }
}
