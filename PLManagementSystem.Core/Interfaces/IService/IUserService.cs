using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Helpers.Helpers;

namespace PLManagementSystem.Core.Interfaces.IService
{
    public interface IUserService
    {
        #region Get
        Task<List<RequestUserDto>> GetAll(bool ignoreIsDeletedQueryFilter = false);
        Task<List<ResponseUserDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false);
        Task<ResponseUserDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<RequestUserDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<PaginationResponseModel> GetAllPaginantion(string? name = null, bool? isActive = null,
            string sortDirection = "asc", string sortColumn = "Id", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false);
        #endregion
        #region Add
        Task<ResponseResult> Create(RequestUserDto dto);
        #endregion
        #region Update
        Task<ResponseResult> Edit(RequestUserDto dto);
        #endregion
        #region Delete

        Task<ResponseResult> SoftDelete(int id);
        Task<ResponseResult> Delete(int id);
        #endregion
    }
}
