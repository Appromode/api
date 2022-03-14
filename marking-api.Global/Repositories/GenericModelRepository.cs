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
    /// <summary>
    /// Generic model interface
    /// </summary>
    /// <typeparam name="T">Extended from a model repository</typeparam>
    public interface IGenericModelRepository<T> where T : class {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T GetById<Type>(Type id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T Add(T obj);
        IEnumerable<T> AddRange(IEnumerable<T> obj);
        T Update(T obj);
        void AddOrUpdate(T obj);
        void AddOrUpdateRange(IEnumerable<T> obj);
        void AddUpdateDeleteRange(IEnumerable<T> obj, IEnumerable<T> existing);
        void Delete(T obj);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> obj);
        void Save();        
        public bool IsBeingTracked<TType>(T entity);
    }

    /// <summary>
    /// Main repository for other model repositories. Contains methods that replace ones provided by the database context
    /// These methods provide more options to manipulate and retrieve data from the database
    /// </summary>
    /// <typeparam name="T">Extended from a model repository</typeparam>
    public class GenericModelRepository<T> : IGenericModelRepository<T> where T : class
    {
        protected readonly MarkingDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public GenericModelRepository(MarkingDbContext dbContext)
        {
            this._dbContext = dbContext;
            _entities = _dbContext.Set<T>();
        }

        /// <summary>
        /// Get data from the database based on extended model repository e.g. Users or Roles.
        /// </summary>
        /// <param name="filter">Llambda expression to filter returned objects by. Use by filter: x => (expression))</param>
        /// <param name="orderBy">Llamba expression to order returned list by. Use by orderby: x => (expression)</param>
        /// <param name="include">Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
        /// <returns>List of objects</returns>
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

        /// <summary>
        /// Get data from the database based on the extended model repository e.g. Users or Roles and the id provided 
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="id">Id of the desired object</param>
        /// <param name="include">Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
        /// <returns>Object</returns>
        public T GetById<Type>(Type id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            return Get(filter: x => id.Equals(EF.Property<Type>(x, idName)), include: include).FirstOrDefault();
        }

        /// <summary>
        /// Get data from the database based on a list of ids and the extended model repository e.g. Users or Roles
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="ids">Ids of the desired objects</param>
        /// <param name="include">Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
        /// <returns>List of Objects</returns>
        public IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            return Get(filter: x => ids.ToList().Contains(EF.Property<Type>(x, idName)), include: include);
        }

        /// <summary>
        /// Get objects based on expression
        /// </summary>
        /// <param name="expression">Llambda expression to filter returned objects</param>
        /// <returns>List of objects</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        /// <summary>
        /// Add object to a table based on the extended model repository e.g. Users or Roles
        /// </summary>
        /// <param name="obj">Object to add to a table</param>
        /// <returns>Added object</returns>
        /// <exception cref="ArgumentNullException">Thrown if object is null</exception>
        public T Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            _entities.Add(obj);

            return obj;
        }

        /// <summary>
        /// Add list of object to a table based on the extended model repository e.g. Users or Roles
        /// </summary>
        /// <param name="objs">Objects to add to a table</param>
        /// <returns>List of added objects</returns>
        /// <exception cref="ArgumentNullException">Thrown if objects are null</exception>
        public IEnumerable<T> AddRange(IEnumerable<T> objs)
        {
            if (objs == null)
                throw new ArgumentNullException("Obj is null");

            _entities.AddRange(objs);

            return objs;
        }

        /// <summary>
        /// Update object in a table based on the extended model repository e.g. Users or Roles
        /// </summary>
        /// <param name="obj">Object to be updated</param>
        /// <returns>Updated object</returns>
        /// <exception cref="ArgumentNullException">Thrown if object is null</exception>
        public T Update(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            _entities.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }

        /// <summary>
        /// Add or update object in a table based on the extended model repository e.g. Users or Roles
        /// Adds the object if the id of the object is not found in the table
        /// Updates the object if the id is found
        /// </summary>
        /// <param name="entity">Object to be added or updated</param>
        /// <exception cref="ArgumentNullException">Thrown if the object is null</exception>
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

        /// <summary>
        /// Add or update objects in a table based on the extended model repository e.g. Users or Roles
        /// Adds the objects if the ids of the objects are not found in the table
        /// Updates the objects if the ids are found
        /// </summary>
        /// <param name="objList">Objects to be added or updated</param>
        /// <exception cref="ArgumentNullException">Thrown if the objects are null</exception>
        public void AddOrUpdateRange(IEnumerable<T> objList)
        {
            Console.WriteLine(objList);

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

        /// <summary>
        /// Add, update or delete list of objects based on the extended model repository e.g. Users or Roles
        /// Adds the objects if the ids are not found in the table
        /// Updates the objects if the ids are found
        /// Deletes the objects if object is 
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="existing"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            if (_dbContext.Entry(obj).State == EntityState.Detached)
                _dbContext.Attach(obj);
            _entities.Remove(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(object id)
        {
            if (id == null)
                throw new ArgumentNullException("Obj is null");

            T existing = _entities.Find(id);
            Delete(existing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void DeleteRange(IEnumerable<T> objs)
        {
            if (objs == null)
                throw new ArgumentNullException("Obj is null");

            _entities.RemoveRange(objs);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
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
