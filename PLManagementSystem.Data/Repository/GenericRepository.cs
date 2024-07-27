using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IDal;
using PLManagementSystem.Data.Entites;
using PLManagementSystem.Helpers.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace PLManagementSystem.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        protected AppDbContext _context;
        protected DbSet<T> entities;
        private IQueryable<T> query;
        private readonly ILogger<GenericRepository<T>> _logger;
        public GenericRepository(AppDbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            entities = _context.Set<T>();
            _logger = logger;
        }

        #region Get

        public async virtual Task<PagedList<T>> GetPagginationItems(List<Expression<Func<T, bool>>> filter,
            Expression<Func<T, object>> sort, string sortDirection = "asc", int pageNumber = 1, int pageSize = 10,
            bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes)
        {

            var query = entities.AsQueryable();

            if (filter != null)

                if (filter.Count > 0)
                {
                    foreach (var item in filter)
                    {
                        query = query.Where(item).AsQueryable();
                    }
                }
            foreach (var include in includes)
                query = query.Include(include);//got to reaffect it.
            if (sort != null)
            {
                if (sortDirection == "asc")
                    query = query.OrderBy(sort);//got to reaffect it.
                else
                    query = query.OrderByDescending(sort);//got to reaffect it.
            }
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            int count = await query.CountAsync();
            int totalPagesCount = (pageSize > 0 && count > 0) ? (int)Math.Ceiling(count / (double)pageSize) : 1;
            if (pageNumber > totalPagesCount || pageNumber == -1)
            {
                pageNumber = totalPagesCount;
            }
            if (pageNumber <= 0 || pageSize <= 0)
                return new PagedList<T>(await query.AsNoTracking().ToListAsync(), count, 0, 0);
            List<T> items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }


        public async virtual Task<List<T>> GetItems(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sort = null,
            string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes)
        {
            List<T> list;
            var query = entities.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);//got to reaffect it.
            if (sort != null)
            {
                if (sortDirection == "asc")
                    query = query.OrderBy(sort);//got to reaffect it
                else
                    query = query.OrderByDescending(sort);//got to reaffect it.
            }
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                list = await query.Where(filter).ToListAsync<T>();
            else
                list = await query.ToListAsync<T>();
            return list;
        }
        public async virtual Task<List<T>> GetItemsAsNoTracking(Expression<Func<T, bool>> filter = null,
            Expression<Func<T, object>> sort = null, string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false,
            params Expression<Func<T, object>>[] includes)
        {
            List<T> list;
            var query = entities.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);//got to reaffect it.
            if (sort != null)
            {
                if (sortDirection == "asc")
                    query = query.OrderBy(sort);//got to reaffect it.
                else
                    query = query.OrderByDescending(sort);//got to reaffect it.
            }
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                list = await query.Where(filter).AsNoTracking().ToListAsync<T>();
            else
                list = await query.AsNoTracking().ToListAsync<T>();
            return list;
        }
        public async virtual Task<List<T>> GetItemsAsNoTrackingWithIncludsAsString(Expression<Func<T, bool>> filter = null,
           Expression<Func<T, object>> sort = null, string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false, string includes = "")
        {
            List<T> list;
            var query = entities.AsQueryable();
            if (!string.IsNullOrEmpty(includes))
            {
                var IncludeList = includes.Split(",", StringSplitOptions.None);
                foreach (var include in IncludeList)
                    query = query.Include(include.Trim());
            }
            if (sort != null)
            {
                if (sortDirection == "asc")
                    query = query.OrderBy(sort);//got to reaffect it.
                else
                    query = query.OrderByDescending(sort);//got to reaffect it.
            }
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                list = await query.Where(filter).AsNoTracking().ToListAsync<T>();
            else
                list = await query.AsNoTracking().ToListAsync<T>();
            return list;
        }
        public async virtual Task<T> GetItem(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false,
            params Expression<Func<T, object>>[] includes)
        {
            T Entite;
            var query = entities.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);//got to reaffect it.
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                Entite = await query.FirstOrDefaultAsync<T>(filter);
            else
                Entite = await query.FirstOrDefaultAsync<T>();
            return Entite;
        }
        public async virtual Task<T> GetItemWithIncludesAsString(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false,
        string includes = "")
        {
            T Entite;
            var query = entities.AsQueryable();
            var IncludeList = includes.Split(",", StringSplitOptions.None);
            foreach (var include in IncludeList)
                query = query.Include(include.Trim());
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                Entite = await query.FirstOrDefaultAsync<T>(filter);
            else
                Entite = await query.FirstOrDefaultAsync<T>();
            return Entite;
        }

        public async virtual Task<T> GetItemAsNoTracking(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false,
            params Expression<Func<T, object>>[] includes)
        {
            T Entite;
            var query = entities.AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);//got to reaffect it.
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                Entite = await query.AsNoTracking().FirstOrDefaultAsync<T>(filter);
            else
                Entite = await query.AsNoTracking().FirstOrDefaultAsync<T>();
            return Entite;
        }

        public async virtual Task<T> GetItemAsNoTrackingWithIncludesAsString(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false,
         string includes = "")
        {
            T Entite;
            var query = entities.AsQueryable();

            var IncludeList = includes.Split(",", StringSplitOptions.None);
            foreach (var include in IncludeList)
                query = query.Include(include.Trim());

            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            if (filter != null)
                Entite = await query.AsNoTracking().FirstOrDefaultAsync<T>(filter);
            else
                Entite = await query.AsNoTracking().FirstOrDefaultAsync<T>();
            return Entite;
        }



        public async virtual Task<int> GetMaxAsNoTracking(Expression<Func<T, object>> filter, bool ignoreIsDeletedQueryFilter = false)
        {
            var query = entities.AsQueryable();
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            var Entite = await query.AsNoTracking().MaxAsync(filter);
            return Entite == null ? 0 : (int)Entite;
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false)
        {
            query = entities.AsQueryable();
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query.AnyAsync(filter);
        }

        public async virtual Task<int> CountAsync(Expression<Func<T, bool>> filter = null, bool ignoreIsDeletedQueryFilter = false)

        {
            query = entities.AsQueryable();
            if (ignoreIsDeletedQueryFilter)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query.CountAsync(filter);

        }

        #endregion
        #region Add

        public async virtual Task<bool> Add(T entity)

        {

            try

            {

                if (entity == null)

                {

                    throw new ArgumentNullException("entity");

                }

                await _context.AddAsync(entity);


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }

        public async virtual Task<bool> AddRange(List<T> entities)

        {

            try

            {

                if (entities == null)

                {

                    throw new ArgumentNullException("entity");

                }

                await _context.AddRangeAsync(entities);


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }

        #endregion

        #region Update
        public bool Update(T entity)

        {

            try

            {

                if (entity == null)

                {

                    throw new ArgumentNullException("entity");

                }

                _context.Update(entity);



                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }
        public bool UpdateRange(List<T> entities)

        {

            try

            {

                if (entities == null)

                {

                    throw new ArgumentNullException("entity");

                }

                _context.UpdateRange(entities);


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }
        #endregion
        #region Delete

        public virtual bool SoftDelete(T entity)
        {
            try
            {
                if (entity == null)

                {

                    throw new ArgumentNullException("entity");

                }

                entity.IsDeleted = true;

                _context.Update(entity);


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }

        public virtual bool Delete(T entity)

        {

            try

            {

                if (entity == null)

                {

                    throw new ArgumentNullException("entity");

                }

                _context.Remove(entity);


                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }

        public virtual bool DeleteRange(List<T> entities)

        {

            try
            {
                if (entities == null)

                {

                    throw new ArgumentNullException("entity");

                }

                _context.RemoveRange(entities);

                return true;

            }

            catch (Exception ex)

            {

                _logger.LogError("database error:", ex);

                return false;

            }

        }
        #endregion

    }



    public class IncludesClass<T> where T : BaseClass
    {



        public Expression<Func<T, object>> include;
        public Expression<Func<T, object>>[] Theninclude;
    }

}
