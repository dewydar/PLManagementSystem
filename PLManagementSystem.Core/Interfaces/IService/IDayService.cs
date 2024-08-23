using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Helpers.Helpers;

namespace PLManagementSystem.Core.Interfaces.IService
{
    public interface IDayService
    {
        #region Get
        Task<List<RequestDayDto>> GetAll(bool ignoreIsDeletedQueryFilter = false);
        Task<List<ResponseDayDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false);
        Task<ResponseDayDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<RequestDayDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<PaginationResponseModel> GetAllPaginantion(string? name = null, bool? isActive = null,
            string sortDirection = "asc", string sortColumn = "Id", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false);
        #endregion
        #region Add
        Task<ResponseResult> Create(RequestDayDto dto);
        #endregion
        #region Update
        Task<ResponseResult> Edit(RequestDayDto dto);
        #endregion
        #region Delete

        Task<ResponseResult> SoftDelete(int id);
        Task<ResponseResult> Delete(int id);
        #endregion
    }
}
