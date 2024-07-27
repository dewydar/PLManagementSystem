using PLManagementSystem.Core.Entities;
using PLManagementSystem.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Interfaces.IDal
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        #region Get
        public Task<T> GetItem(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes);
        public Task<T> GetItemWithIncludesAsString(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false, string includes = "");
        public Task<T> GetItemAsNoTracking(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes);
        public Task<T> GetItemAsNoTrackingWithIncludesAsString(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false, string includes = "");
        public Task<int> GetMaxAsNoTracking(Expression<Func<T, object>> filter, bool ignoreIsDeletedQueryFilter = false);
        public Task<PagedList<T>> GetPagginationItems(List<Expression<Func<T, bool>>> filter,
            Expression<Func<T, object>> sort, string sortDirection = "asc", int pageNumber = 1, int pageSize = 10,
            bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes);

        public Task<List<T>> GetItems(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sort = null,
            string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes);
        public Task<List<T>> GetItemsAsNoTracking(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sort = null,
            string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false, params Expression<Func<T, object>>[] includes);
        public Task<List<T>> GetItemsAsNoTrackingWithIncludsAsString(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> sort = null,
            string sortDirection = "asc", bool ignoreIsDeletedQueryFilter = false, string includes = "");
        public Task<int> CountAsync(Expression<Func<T, bool>> filter = null, bool ignoreIsDeletedQueryFilter = false);
        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool ignoreIsDeletedQueryFilter = false);
        #endregion

        #region Add
        public Task<bool> Add(T entity);
        public Task<bool> AddRange(List<T> entities);
        #endregion

        #region Update
        public bool Update(T entity);
        public bool UpdateRange(List<T> entities);
        #endregion

        #region Delete
        public bool SoftDelete(T entity);
        public bool Delete(T entity);
        public bool DeleteRange(List<T> entities);
        #endregion

    }
}
