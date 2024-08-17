using AutoMapper;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Helpers.Enum;
using PLManagementSystem.Helpers.Helpers;
using PLManagementSystem.Helpers.ResourceFiles;

namespace PLManagementSystem.service.Services
{
    public class LessonGroupsDaysService : ILessonGroupsDaysService
    {
        private readonly IDataWrapper _dataWrapper;
        private readonly IMapper _Mapper;
        public LessonGroupsDaysService(IDataWrapper dataWrapper, IMapper mapper)
        {
            _dataWrapper = dataWrapper;
            _Mapper = mapper;
        }
        #region Get
        public async Task<RequestLessonGroupsDaysDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.LessonGroupsDaysRepository.GetItem(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);
            return _Mapper.Map<RequestLessonGroupsDaysDto>(entity);
        }
        public async Task<List<ResponseLessonGroupsDaysDto>> GetAll(int lessonGroupId, bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.LessonGroupsDaysRepository.GetItemsAsNoTracking(filter: z => z.LessonGroupId == lessonGroupId,
                ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter, includes: z => z.Dayes);
            return _Mapper.Map<List<ResponseLessonGroupsDaysDto>>(entities);
        }
        #endregion
        #region Add
        public async Task<ResponseResult> Create(RequestLessonGroupsDaysDto dto)
        {
            if (!await this.IfExist(dto.LessonGroupId, dto.DayId, dto.Id))
            {

                var entity = _Mapper.Map<LessonGroupsDays>(dto);
                var MaxId = await _dataWrapper.LessonGroupsDaysRepository.GetMaxAsNoTracking(filter: z => z.Id, ignoreIsDeletedQueryFilter: true);
                entity.Id = MaxId + 1;
                entity.IsDeleted = false;
                await _dataWrapper.LessonGroupsDaysRepository.Add(entity);
                await _dataWrapper.UnitOfWork.Commit();
                ResponseResult response = new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseLessonGroupsDaysDto>(entity),
                    Message = UI.AddSuccess
                };
                return response;
            }
            else
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.BadRequest,
                    IsSucceeded = false,
                    Message = UI.DuplicateData
                };
        }
        #endregion
        #region Edit
        public async Task<ResponseResult> Edit(RequestLessonGroupsDaysDto dto)
        {
            if (!await this.IfExist(dto.LessonGroupId, dto.DayId, dto.Id))
            {
                var entity = _Mapper.Map<LessonGroupsDays>(dto);
                _dataWrapper.LessonGroupsDaysRepository.Update(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseLessonGroupsDaysDto>(entity),
                    Message = UI.EditSuccess
                };
            }
            else
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.BadRequest,
                    IsSucceeded = false,
                    Message = UI.DuplicateData
                };
        }
        #endregion

        public async Task<ResponseResult> Delete(int id)
        {
            var entity = await _dataWrapper.LessonGroupsDaysRepository.GetItemAsNoTracking(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: true);
            if (entity == null)
            {
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.NotFound,
                    IsSucceeded = false,
                    Message = UI.ErrorNotFound
                };
            }
            else
            {
                _dataWrapper.LessonGroupsDaysRepository.Delete(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    Message = UI.DeleteSuccess
                };
            }
        }
        #region Helpers
        public async Task<List<ResponseDayDto>> DayesList()
        {
            var list = await _dataWrapper.DayRepository.GetItemsAsNoTracking();
            return _Mapper.Map<List<ResponseDayDto>>(list);
        }
        private async Task<bool> IfExist(int lessonGroupId, int dayId, int? id = 0)
        {
            var entity = await _dataWrapper.LessonGroupsDaysRepository.GetItemAsNoTracking(z => (id == 0 || z.Id != id)
            && z.LessonGroupId == lessonGroupId && z.DayId == dayId);
            return entity != null ? true : false;
        }
        #endregion
    }
}
