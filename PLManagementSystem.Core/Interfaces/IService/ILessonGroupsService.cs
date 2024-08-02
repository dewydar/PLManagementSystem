using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Dtos;
using PLManagementSystem.Helpers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLManagementSystem.Core.Entities;

namespace PLManagementSystem.Core.Interfaces.IService
{
    public interface ILessonGroupsService
    {
        #region Get
        Task<List<RequestLessonGroupsDto>> GetAll(bool ignoreIsDeletedQueryFilter = false);
        Task<List<ResponseLessonGroupsDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false);
        Task<ResponseLessonGroupsDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<RequestLessonGroupsDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<PaginationResponseModel> GetAllPaginantion(string? name = null, int? classId = null, int? dayId = null, string sortDirection = "asc",
            string sortColumn = "OrderNo", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false);
        #endregion
        #region Add
        Task<ResponseResult> Create(RequestLessonGroupsDto dto);
        #endregion
        #region Update
        Task<ResponseResult> Edit(RequestLessonGroupsDto dto);
        #endregion
        #region Delete

        Task<ResponseResult> SoftDelete(int id);
        Task<ResponseResult> Delete(int id);
        #endregion

        #region Change Position
        Task<ResponseResult> UpdateOrderAsync(int id, UpdateOrderDto dto);
        #endregion
        #region Helpers
        Task<List<ResponseClassDto>> ClassesList(); 
        Task<List<ResponseDayDto>> DayesList(); 
        #endregion
    }
}
