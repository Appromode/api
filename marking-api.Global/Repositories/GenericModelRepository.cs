using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using marking_api.Data;
using marking_api.Global.Extensions;
using marking_api.Global.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace marking_api.Global.Repositories
{
    /// <summary>
    /// Generic model interface
    /// </summary>
    /// <typeparam name="T">Extended from a model repository</typeparam>
    public interface IGenericModelRepository<T> where T : class {
        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="filter">Expression(Func(T, bool))</param>
        /// <param name="orderBy">Func(IQueryable(T), IOrderedQueryable(T))</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object))</param>
        /// <returns>IEnumerable(T)</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Get by id method
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="id"></param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object))</param>
        /// <returns></returns>
        T GetById<Type>(Type id, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Get by ids method
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="ids"></param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object))</param>
        /// <returns></returns>
        IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Find method
        /// </summary>
        /// <param name="expression">Expression(Func(T, bool))</param>
        /// <returns>Found object</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Added object</returns>
        T Add(T obj);
        /// <summary>
        /// Add objects method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Added objects</returns>
        IEnumerable<T> AddRange(IEnumerable<T> obj);
        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Updated method</returns>
        T Update(T obj);
        /// <summary>
        /// Add or update method
        /// </summary>
        /// <param name="obj"></param>
        void AddOrUpdate(T obj);
        /// <summary>
        /// Add or updated objects method
        /// </summary>
        /// <param name="obj"></param>
        void AddOrUpdateRange(IEnumerable<T> obj);
        /// <summary>
        /// Add, update or delete objects
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="existing"></param>
        void AddUpdateDeleteRange(IEnumerable<T> obj, IEnumerable<T> existing);
        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="obj"></param>
        void Delete(T obj);
        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// Delete objects
        /// </summary>
        /// <param name="obj"></param>
        void DeleteRange(IEnumerable<T> obj);
        /// <summary>
        /// Save method
        /// </summary>
        void Save();        
        /// <summary>
        /// Is object being tracked
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsBeingTracked<TType>(T entity);
    }

    /// <summary>
    /// Main repository for other model repositories. Contains methods that replace ones provided by the database context
    /// These methods provide more options to manipulate and retrieve data from the database
    /// </summary>
    /// <typeparam name="T">Extended from a model repository</typeparam>
    public class GenericModelRepository<T> : IGenericModelRepository<T> where T : class
    {
        private readonly MarkingDbContext _dbContext;
        private readonly DataFilterService _dfService;
        private ILogger _logger;
        private readonly DbSet<T> _entities;

        /// <summary>
        /// GenericModelRepository constructor
        /// </summary>
        /// <param name="dbContext">MarkingDbContext</param>
        /// <param name="dfService">DataFilterService</param>
        public GenericModelRepository(MarkingDbContext dbContext, DataFilterService dfService, ILogger logger)
        {
            this._dbContext = dbContext;
            _entities = _dbContext.Set<T>();
            _dfService = dfService;
        }

        /// <summary>
        /// Get data from the database based on extended model repository e.g. Users or Roles.
        /// </summary>
        /// <param name="filter">Expression(Func(T, bool)) - Llambda expression to filter returned objects by. Use by filter: x => (expression))</param>
        /// <param name="orderBy">Func(IQueryable(T), IOrderedQueryable(T)) - Llamba expression to order returned list by. Use by orderby: x => (expression)</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object)) - Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
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
        /// <param name="id">Type - Id of the desired object</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object)) - Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
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
        /// <param name="ids">IEnumerable(Type) - Ids of the desired objects</param>
        /// <param name="include">Func(IQueryable(T), IIncludableQueryable(T, object)) - Llambda expression to include and populate objects within the base objects retrieved. Use by include: x => (expression)</param>
        /// <returns>List of Objects</returns>
        public IEnumerable<T> GetByIds<Type>(IEnumerable<Type> ids, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;
            return Get(filter: x => ids.ToList().Contains(EF.Property<Type>(x, idName)), include: include);
        }

        /// <summary>
        /// Get objects based on expression
        /// </summary>
        /// <param name="expression">Expression(Func(T, bool)) - Llambda expression to filter returned objects</param>
        /// <returns>List of objects</returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }

        /// <summary>
        /// Add object to a table based on the extended model repository e.g. Users or Roles
        /// </summary>
        /// <param name="obj">T - Object to add to a table</param>
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
        /// <param name="objs">IEnumerable(T) - Objects to add to a table</param>
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
        /// <param name="obj">T - Object to be updated</param>
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
        /// <param name="entity">T - Object to be added or updated</param>
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
        /// <param name="objList">IEnumberable(T) - Objects to be added or updated</param>
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
        /// Deletes the objects if object is not found in objList and is found in existing
        /// </summary>
        /// <param name="objList">IEnumerable(T) - </param>
        /// <param name="existing"></param>
        public void AddUpdateDeleteRange(IEnumerable<T> objList, IEnumerable<T> existing)
        {
            var idName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Single().Name;

            List<T> deletedItems = new List<T>();

            AddOrUpdateRange(objList);
            foreach (var entity in existing)
            {
                if (!objList.Any(x => x.GetType().GetProperty(idName).GetValue(x).Equals(entity.GetType().GetProperty(idName).GetValue(entity))))
                {
                    var deleteMethod = entity.GetType().GetMethod("delete");
                    if (deleteMethod != null)
                    {
                        deleteMethod.Invoke(entity, new object[0]);
                        deletedItems.Add(entity);
                    }
                    else
                        Delete(entity);
                }
            }
            AddOrUpdateRange(deletedItems);
            Save();
        }

        /// <summary>
        /// Hard delete object within the database 
        /// </summary>
        /// <param name="obj">T - Object to be deleted</param>
        /// <exception cref="ArgumentNullException">Thrown if obj is null</exception>
        public void Delete(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Obj is null");

            if (_dbContext.Entry(obj).State == EntityState.Detached)
                _dbContext.Attach(obj);
            _entities.Remove(obj);
        }

        /// <summary>
        /// Hard delete an object based on the id
        /// </summary>
        /// <param name="id">object - Id of the object</param>
        /// <exception cref="ArgumentNullException">Thrown if id is null</exception>
        public void Delete(object id)
        {
            if (id == null)
                throw new ArgumentNullException("Obj is null");

            T existing = _entities.Find(id);
            Delete(existing);
        }

        /// <summary>
        /// Hard delete range of objects within the database
        /// </summary>
        /// <param name="objs">IEnumerable(T) - List of objects to delete</param>
        /// <exception cref="ArgumentNullException">Thrown if list is null</exception>
        public void DeleteRange(IEnumerable<T> objs)
        {
            if (objs == null)
                throw new ArgumentNullException("Obj is null");

            _entities.RemoveRange(objs);
        }

        /// <summary>
        /// Save changes made to the database using database context savechanges method
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }        

        /// <summary>
        /// Check if an object is being tracked by entity framework.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="entity">T - entity to check</param>
        /// <returns>True if entity is being tracked by entity framework</returns>
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
                _logger.LogError(ex, "Error checking if entity is being tracked");
                return true;
            }
        }
    }
}
