using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Helpers.Helpers;

namespace PLManagementSystem.Core.Interfaces.IService
{
    public interface ILessonGroupsDaysService
    {
        #region Get
        Task<RequestLessonGroupsDaysDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false);
        Task<List<ResponseLessonGroupsDaysDto>> GetAll(int lessonGroupId, bool ignoreIsDeletedQueryFilter = false);
        #endregion
        #region Add
        Task<ResponseResult> Create(RequestLessonGroupsDaysDto dto);
        #endregion
        #region Update
        Task<ResponseResult> Edit(RequestLessonGroupsDaysDto dto);
        #endregion
        #region Delete
        Task<ResponseResult> Delete(int id);
        #endregion
        #region Helpers
        Task<List<ResponseDayDto>> DayesList();
        #endregion
    }
}
