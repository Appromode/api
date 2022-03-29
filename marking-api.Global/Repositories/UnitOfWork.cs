using log4net;
using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories.Models;
using marking_api.Global.Services;
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
        private readonly DataFilterService _dfService;
        private ILog _logger;

        /// <summary>
        /// Initiate repositories, database context, signin manager and config
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="signInManager">Identity signin manager</param>
        /// <param name="config">Application configuration</param>
        public UnitOfWork(MarkingDbContext dbContext, SignInManager<User> signInManager, IConfiguration config, DataFilterService dfService, ILog logger)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _config = config;
            _dfService = dfService;
            _logger = logger;

            //Models
            Audits = new AuditRepository(dbContext, dfService, logger);
            Comments = new CommentRepository(dbContext, dfService, logger);
            FSFiles = new FSFileRepository(dbContext, dfService, logger);
            FSFileStates = new FSFileStateRepository(dbContext, dfService, logger);
            FSFileVersions = new FSFileVersionRepository(dbContext, dfService, logger);
            FSFolderFiles = new FSFolderFileRepository(dbContext, dfService, logger);
            FSFolders = new FSFolderRepository(dbContext, dfService, logger);
            FSFolderRoles = new FSFolderRoleRepository(dbContext, dfService, logger);
            Grades = new GradeRepository(dbContext, dfService, logger);
            GroupMarkers = new GroupMarkerRepository(dbContext, dfService, logger);
            Groups = new GroupRepository(dbContext, dfService, logger);
            Links = new LinkRepository(dbContext, dfService, logger);
            Logs = new LogRepository(dbContext, dfService, logger);
            Feedback = new FeedbackRepository(dbContext, dfService, logger);
            Projects = new ProjectRepository(dbContext, dfService, logger);
            RefreshTokens = new RefreshTokenRepository(dbContext, dfService, logger);
            RoleClaims = new RoleClaimRepository(dbContext, dfService, logger);
            RoleLinks = new RoleLinkRepository(dbContext, dfService, logger);
            RolePermissions = new RolePermissionRepository(dbContext, dfService, logger);
            Roles = new RoleRepository(dbContext, dfService, logger);
            SiteAreas = new SiteAreaRepository(dbContext, dfService, logger);
            Tags = new TagRepository(dbContext, dfService, logger);
            Threads = new ThreadRepository(dbContext, dfService, logger);
            UserClaims = new UserClaimRepository(dbContext, dfService, logger);
            UserGrades = new UserGradeRepository(dbContext, dfService, logger);
            UserGroups = new UserGroupRepository(dbContext, dfService, logger);
            UserLogins = new UserLoginRepository(dbContext, dfService, logger);
            Users = new UserRepository(dbContext, dfService, logger);
            UserRoles = new UserRoleRepository(dbContext, dfService, logger);
            UserTokens = new UserTokenRepository(dbContext, dfService, logger);
            UserTags = new UserTagRepository(dbContext, dfService, logger);
            Invites = new InvitesRepository(dbContext, dfService, logger);
            GroupTags = new GroupTagRepository(dbContext, dfService, logger);

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
