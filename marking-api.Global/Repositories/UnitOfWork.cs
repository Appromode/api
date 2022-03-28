using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace marking_api.Global.Repositories
{
    /// <summary>
    /// UnitOfWork interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        //UnitOfWork repository interfaces
        /// <summary>
        /// Audit repository interface
        /// </summary>
        IAuditRepository Audits { get; }
        /// <summary>
        /// File repository interface
        /// </summary>
        IFSFileRepository FSFiles { get; }
        /// <summary>
        /// File state repository interface
        /// </summary>
        IFSFileStateRepository FSFileStates { get; }
        /// <summary>
        /// File version repository interface
        /// </summary>
        IFSFileVersionRepository FSFileVersions { get; }
        /// <summary>
        /// Folder file repository interface
        /// </summary>
        IFSFolderFileRepository FSFolderFiles { get; }
        /// <summary>
        /// Folder repository interface
        /// </summary>
        IFSFolderRepository FSFolders { get; }
        /// <summary>
        /// Folder role repository interface
        /// </summary>
        IFSFolderRoleRepository FSFolderRoles { get; }
        /// <summary>
        /// Comment repository interface
        /// </summary>
        ICommentRepository Comments { get; }
        /// <summary>
        /// Generic method repository interface
        /// </summary>
        IGenericMethodRepository GenericMethods { get; }
        /// <summary>
        /// Grade repository interface
        /// </summary>
        IGradeRepository Grades { get; }
        /// <summary>
        /// Group markers repository interface
        /// </summary>
        IGroupMarkerRepository GroupMarkers { get; }
        /// <summary>
        /// Group repository interface
        /// </summary>
        IGroupRepository Groups { get; }
        /// <summary>
        /// Link repository interface
        /// </summary>
        ILinkRepository Links { get; }
        /// <summary>
        /// Log repository interface
        /// </summary>
        ILogRepository Logs { get; }
        /// <summary>
        /// Feedback repository interface
        /// </summary>
        IFeedbackRepository Feedback { get; }
        /// <summary>
        /// Project repository interface
        /// </summary>
        IProjectRepository Projects { get; }
        /// <summary>
        /// Refresh token repository interface
        /// </summary>
        IRefreshTokenRepository RefreshTokens { get; }
        /// <summary>
        /// Role claim repository interface
        /// </summary>
        IRoleClaimRepository RoleClaims { get; }
        /// <summary>
        /// Role link repository interface
        /// </summary>
        IRoleLinkRepository RoleLinks { get; }
        /// <summary>
        /// Role permission repository interface
        /// </summary>
        IRolePermissionRepository RolePermissions { get; }
        /// <summary>
        /// Role repository interface
        /// </summary>
        IRoleRepository Roles { get; }    
        /// <summary>
        /// Site area repository interface
        /// </summary>
        ISiteAreaRepository SiteAreas { get; }
        /// <summary>
        /// Tag repository interface
        /// </summary>
        ITagRepository Tags { get; }
        /// <summary>
        /// Thread repository interface
        /// </summary>
        IThreadRepository Threads { get; }
        /// <summary>
        /// User claim repository interface
        /// </summary>
        IUserClaimRepository UserClaims { get; }
        /// <summary>
        /// User grade repository interface
        /// </summary>
        IUserGradeRepository UserGrades { get; }
        /// <summary>
        /// User group repository interface
        /// </summary>
        IUserGroupRepository UserGroups { get; }
        /// <summary>
        /// User login repository interface
        /// </summary>
        IUserLoginRepository UserLogins { get; }
        /// <summary>
        /// User repository interface
        /// </summary>
        IUserRepository Users { get; }
        /// <summary>
        /// User role repository interface
        /// </summary>
        IUserRoleRepository UserRoles { get; }
        /// <summary>
        /// User token repository interface
        /// </summary>
        IUserTokenRepository UserTokens { get; }
        /// <summary>
        /// User tag repository interface
        /// </summary>
        IUserTagRepository UserTags { get; }  
        /// <summary>
        /// Invites repository interface
        /// </summary>
        IInvitesRepository Invites { get; }

        IGroupTagRepository GroupTags { get; }

        //Views

        /// <summary>
        /// Save method
        /// </summary>
        /// <returns>Base Save method</returns>
        int Save();
    }

    /// <summary>
    /// Main unitofwork class. Provides an abstraction layer between the database context and the controllers of the application
    /// This gives more control over how the data from the database is manipulated and control what data is accessible within the database
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarkingDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initiate repositories, database context, signin manager and config
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="signInManager">Identity signin manager</param>
        /// <param name="config">Application configuration</param>
        public UnitOfWork(MarkingDbContext dbContext, SignInManager<User> signInManager, IConfiguration config)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _config = config;

            //Models
            Audits = new AuditRepository(dbContext);
            Comments = new CommentRepository(dbContext);
            FSFiles = new FSFileRepository(dbContext);
            FSFileStates = new FSFileStateRepository(dbContext);
            FSFileVersions = new FSFileVersionRepository(dbContext);
            FSFolderFiles = new FSFolderFileRepository(dbContext);
            FSFolders = new FSFolderRepository(dbContext);
            FSFolderRoles = new FSFolderRoleRepository(dbContext);
            Grades = new GradeRepository(dbContext);
            GroupMarkers = new GroupMarkerRepository(dbContext);
            Groups = new GroupRepository(dbContext);
            Links = new LinkRepository(dbContext);
            Logs = new LogRepository(dbContext);
            Feedback = new FeedbackRepository(dbContext);
            Projects = new ProjectRepository(dbContext);
            RefreshTokens = new RefreshTokenRepository(dbContext);
            RoleClaims = new RoleClaimRepository(dbContext);
            RoleLinks = new RoleLinkRepository(dbContext);
            RolePermissions = new RolePermissionRepository(dbContext);
            Roles = new RoleRepository(dbContext);
            SiteAreas = new SiteAreaRepository(dbContext);
            Tags = new TagRepository(dbContext);
            Threads = new ThreadRepository(dbContext);
            UserClaims = new UserClaimRepository(dbContext);
            UserGrades = new UserGradeRepository(dbContext);
            UserGroups = new UserGroupRepository(dbContext);
            UserLogins = new UserLoginRepository(dbContext);
            Users = new UserRepository(dbContext);
            UserRoles = new UserRoleRepository(dbContext);
            UserTokens = new UserTokenRepository(dbContext);
            UserTags = new UserTagRepository(dbContext);
            Invites = new InvitesRepository(dbContext);
            GroupTags = new GroupTagRepository(dbContext);

            GenericMethods = new GenericMethodRepository(dbContext, config);

            //Views
        }

        //Models
        public IAuditRepository Audits { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IFSFileRepository FSFiles { get; private set; }
        public IFSFileStateRepository FSFileStates { get; private set; }
        public IFSFileVersionRepository FSFileVersions { get; private set; }
        public IFSFolderFileRepository FSFolderFiles { get; private set; }
        public IFSFolderRepository FSFolders { get; private set; }
        public IFSFolderRoleRepository FSFolderRoles { get; private set; }
        public IGradeRepository Grades { get; private set; }
        public IGroupMarkerRepository GroupMarkers { get; private set; }
        public IGroupRepository Groups { get; private set; }
        public ILinkRepository Links { get; private set; }
        public ILogRepository Logs { get; private set; }
        public IFeedbackRepository Feedback { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }
        public IRoleClaimRepository RoleClaims { get; private set; }
        public IRoleLinkRepository RoleLinks { get; private set; }
        public IRolePermissionRepository RolePermissions { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ISiteAreaRepository SiteAreas { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IThreadRepository Threads { get; private set; }
        public IUserClaimRepository UserClaims { get; private set; }
        public IUserGradeRepository UserGrades { get; private set; }
        public IUserGroupRepository UserGroups { get; private set; }
        public IUserLoginRepository UserLogins { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserRoleRepository UserRoles { get; private set; }
        public IUserTokenRepository UserTokens { get; private set; }
        public IUserTagRepository UserTags { get; private set; }
        public IGenericMethodRepository GenericMethods { get; private set; }
        public IInvitesRepository Invites { get; private set; }
        public IGroupTagRepository GroupTags { get; private set; }

        //Views
        public int Save() => _dbContext.SaveChanges();
        public void Dispose() => _dbContext.Dispose();
    }
}
