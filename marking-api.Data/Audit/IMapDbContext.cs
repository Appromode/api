using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Data.Audit
{
    /// <summary>
    /// Interface for the database context. Extends audit database context and idisposable
    /// </summary>
    public interface IMapDbContext : IAuditDbContext, IDisposable
    {
        /// <summary>
        /// Access to the database context and database
        /// </summary>
        DatabaseFacade Database { get; }
        /// <summary>
        /// Save changes method to save data within the database
        /// </summary>
        /// <returns>Base Savechanges method</returns>       
       
        int SaveChanges();
    }
}
