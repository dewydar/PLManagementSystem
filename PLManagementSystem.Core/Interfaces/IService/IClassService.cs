using PLManagementSystem.Core.Dtos;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Helpers.Enum;
using PLManagementSystem.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.Core.Interfaces.IService
{
    public interface IClassService
    {
        #region Get
        Task<List<RequestClassDto>> GetAll(bool ignoreIsDeletedQueryFilter = false);
        Task<List<ResponseClassDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false);
        Task<ResponseClassDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<RequestClassDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<PaginationResponseModel> GetAllPaginantion(string? name = null, bool? isActive = null,
            string sortDirection = "asc", string sortColumn = "OrderNo", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false);
        #endregion
        #region Add
        Task<ResponseResult> Create(RequestClassDto dto);
        #endregion
        #region Update
        Task<ResponseResult> Edit(RequestClassDto dto);
        #endregion
        #region Delete

        Task<ResponseResult> SoftDelete(int id);
        Task<ResponseResult> Delete(int id);
        #endregion

        #region Change Position
        Task<ResponseResult> UpdateOrderAsync(int id, UpdateOrderDto dto);
        #endregion
    }
}
