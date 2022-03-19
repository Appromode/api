using marking_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories
{
    /// <summary>
    /// Generic view interface
    /// </summary>
    /// <typeparam name="T">Extended from a view repository</typeparam>
    public interface IGenericViewRepository<T> where T : class
    {
        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="filter">Expression(Func(T, bool))</param>
        /// <param name="orderBy">Func(IQueryable(T), IOrderedQueryable(T))</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object))</param>
        /// <returns>IEnumerable(T)</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }

    /// <summary>
    /// Provides a method to use database views to retrieve many tables worth of data at once
    /// Main repository for other view repositories. Contains methods that replace ones provided by the database context
    /// These methods provide more options to maniplulate and retrieve data from the database
    /// </summary>
    /// <typeparam name="T">Extended from a view repository</typeparam>
    public class GenericViewRepository<T> : IGenericViewRepository<T> where T : class
    {
        private readonly MarkingDbContext _dbContext;
        private readonly DbSet<T> _entities;

        /// <summary>
        /// GenericViewRepository constructor
        /// </summary>
        /// <param name="dbContext">MarkingDbContext</param>
        public GenericViewRepository(MarkingDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        /// <summary>
        /// Get data from the database based on the extended view repository e.g. (Threads + comments + projects)
        /// </summary>
        /// <param name="filter">Expression(Func(T, bool)) - Llambda expression to filter returned objects by. Use by filter: x => (expression))</param>
        /// <param name="orderBy">Func(IQueryable(T), IOrderedQueryable(T)) - Llamba expression to order returned list by. Use by orderby: x => (expression)</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object)) - Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _entities;

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToList();
        }
    }
}
