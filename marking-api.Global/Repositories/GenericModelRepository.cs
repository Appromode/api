using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using marking_api.Data;
using marking_api.Global.Extensions;
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
        void AddOrUpdate(T obj);
        void AddOrUpdateRange(IEnumerable<T> obj);
        void AddUpdateDeleteRange(IEnumerable<T> obj, IEnumerable<T> existing);
        void Delete(T obj);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> obj);
        void StartTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void Save();        
        public bool IsBeingTracked<TType>(T entity);
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

            return query.AsNoTracking().ToList();
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

        public void AddOrUpdate(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity is null");

            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            var id = typeof(T).GetProperty(idName).GetValue(entity, null);

            var a = _entities.Find(id);

            if (a == null)
                _entities.Add(entity);
            else
                _dbContext.Entry(a).CurrentValues.SetValues(entity);

            Save();
        }

        public void AddOrUpdateRange(IEnumerable<T> objList)
        {
            if (objList == null)
                throw new ArgumentNullException("objList is null");

            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            var idProp = typeof(T).GetProperty(idName);
            foreach (var entity in objList)
            {
                var id = idProp.GetValue(entity, null);
                var a = _entities.Find(id);
                if (a == null)
                    _entities.Add(a);
                else
                    _dbContext.Entry(a).CurrentValues.SetValues(entity);
            }
            Save();
        }

        public void AddUpdateDeleteRange(IEnumerable<T> objList, IEnumerable<T> existing)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;

            AddOrUpdateRange(objList);
            foreach (var entity in existing)
            {
                if (existing.Any(x => x.GetType().GetProperty(idName) == entity.GetType().GetProperty(idName)))
                    Delete(entity);
            }
            Save();
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
                throw new ArgumentNullException("Obj is null");

            _entities.RemoveRange(objs);
        }

        public void StartTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }        

        public bool IsBeingTracked<TType>(T entity)
        {
            try
            {
                var idKey = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey();
                var idName = idKey.Properties.Single().Name;
                var entityId = entity.GetPropertyValue<TType>(idName);
                var entries = _dbContext.ChangeTracker.Entries<T>().ToList();
                var isTracked = entries.Any(x => x.Entity.GetPropertyValue<TType>(idName).Equals(entityId));

                return isTracked;
            } catch (Exception ex)
            {
                return true;
            }
        }
    }
}
