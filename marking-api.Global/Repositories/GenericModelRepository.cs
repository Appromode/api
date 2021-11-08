using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using marking_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace marking_api.Global.Repositories
{
    public interface IGenericModelRepository<T> where T : class {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T GetById<Type>(Type id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T obj);
        void AddRange(IEnumerable<T> obj);
        void Update(T obj);
        void Delete(T obj);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> obj);
        void Save();
    }

    public class GenericModelRepository<T> : IGenericModelRepository<T> where T : class
    {
        protected readonly MarkingDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public GenericModelRepository(MarkingDbContext dbContext)
        {
            this._dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

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

        public T GetById<Type>(Type id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            return Get(filter: x => id.Equals(EF.Property<Type>(x, idName)), include: include).FirstOrDefault();
        }

        public IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            return Get(filter: x => ids.ToList().Contains(EF.Property<Type>(x, idName)), include: include);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        public void Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            _entities.Add(obj);
        }

        public void AddRange(IEnumerable<T> objs)
        {
            if (objs == null)
                throw new ArgumentNullException("Obj is null");

            _entities.AddRange(objs);
        }

        public void Update(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            _entities.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            if (_dbContext.Entry(obj).State == EntityState.Detached)
                _dbContext.Attach(obj);
            _entities.Remove(obj);
        }

        public void Delete(object id)
        {
            if (id == null)
                throw new ArgumentNullException("Obj is null");

            T existing = _entities.Find(id);
            Delete(existing);
        }

        public void DeleteRange(IEnumerable<T> objs)
        {
            if (objs == null)
                throw new ArgumentNullException("Obj is null");            _entities.RemoveRange(objs);        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
