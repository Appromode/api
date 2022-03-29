using log4net;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Models.Project
{
    /// <summary>
    /// Tag controller model
    /// </summary>
    public class TagCM : BaseModel
    {
        /// <summary>
        /// UnitOfWork database access
        /// </summary>
        public IUnitOfWork _unitOfWork;
        
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfwork">IUnitOfWork</param>
        public TagCM(IUnitOfWork unitOfwork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfwork;
        }
    }
}