using AutoMapper;
using PLManagementSystem.Core.Dtos;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Helpers.Enum;
using PLManagementSystem.Helpers.Helpers;
using PLManagementSystem.Helpers.ResourceFiles;
using System.Linq.Expressions;

namespace PLManagementSystem.service.Services
{
    public class LessonGroupsService : ILessonGroupsService
    {
        private readonly IDataWrapper _dataWrapper;
        private readonly IMapper _Mapper;
        public LessonGroupsService(IDataWrapper dataWrapper, IMapper mapper)
        {
            _dataWrapper = dataWrapper;
            _Mapper = mapper;
        }
        #region Get
        public async Task<RequestLessonGroupsDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItem(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                includes: new Expression<Func<LessonGroups, object>>[] { z => z.Classes });
            return _Mapper.Map<RequestLessonGroupsDto>(entity);
        }
        public async Task<ResponseLessonGroupsDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItemAsNoTracking(filter: z => z.Id == id,
                ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                includes: new Expression<Func<LessonGroups, object>>[] { z => z.Classes });
            return _Mapper.Map<ResponseLessonGroupsDto>(entity);
        }
        public async Task<List<RequestLessonGroupsDto>> GetAll(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.LessonGroupsRepository.GetItems(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<RequestLessonGroupsDto>>(entities);
        }
        public async Task<List<ResponseLessonGroupsDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.LessonGroupsRepository.GetItemsAsNoTracking(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<ResponseLessonGroupsDto>>(entities);
        }
        public async Task<PaginationResponseModel> GetAllPaginantion(string? name = null, int? classId = null, int? dayId = null, string sortDirection = "asc",
            string sortColumn = "OrderNo", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false)
        {
            Expression<Func<LessonGroups, object>> Sort = null;
            switch (sortColumn)
            {
                case "Id":
                    Sort = e => e.Id;
                    break;
                case "Name":
                    Sort = e => e.Name;
                    break;
                case "OrderNo":
                    Sort = e => e.OrderNo;
                    break;
                case "ClassName":
                    Sort = e => e.ClassId;
                    break;
                default:
                    Sort = e => e.OrderNo;
                    break;
            }
            List<Expression<Func<LessonGroups, bool>>> search = new List<Expression<Func<LessonGroups, bool>>>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                search.Add(z => z.Name.ToLower().Contains(name.ToLower())
                || z.Name.ToLower() == name.ToLower());
            }
            if (classId != null)
            {
                search.Add(z => z.ClassId == classId);
            }
            if (dayId != null)
            {
                search.Add(z => z.LessonGroupsDays.Where(z => z.DayId == dayId).ToList() != null);
            }
            PagedList<LessonGroups> PagedResult = await _dataWrapper.LessonGroupsRepository.GetPagginationItems(search, Sort, sortDirection, offset, limit,
                ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                includes: new Expression<Func<LessonGroups, object>>[] { z => z.Classes, z => z.LessonGroupsDays });

            var result = _Mapper.Map<List<ResponseLessonGroupsDto>>(PagedResult.ToList());

            if (!result.Any()) return new PaginationResponseModel(offset, null, limit, PagedResult.TotalCount, PagedResult.TotalPages);

            return new PaginationResponseModel(offset, result, limit, PagedResult.TotalCount, PagedResult.TotalPages);
        }

        #endregion
        #region Create
        public async Task<ResponseResult> Create(RequestLessonGroupsDto dto)
        {
            if (!await IfExist(dto.Name, dto.Id))
            {
                LessonGroups entity = _Mapper.Map<LessonGroups>(dto);
                var MaxId = await _dataWrapper.LessonGroupsRepository.GetMaxAsNoTracking(filter: z => z.Id, ignoreIsDeletedQueryFilter: true);
                entity.Id = MaxId + 1;
                var MaxOrderNo = await _dataWrapper.LessonGroupsRepository.GetMaxAsNoTracking(filter: z => z.OrderNo);
                entity.OrderNo = MaxOrderNo + 1;
                entity.IsDeleted = false;
                await _dataWrapper.LessonGroupsRepository.Add(entity);
                await _dataWrapper.UnitOfWork.Commit();
                ResponseResult response = new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseLessonGroupsDto>(entity),
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
        public async Task<ResponseResult> Edit(RequestLessonGroupsDto dto)
        {
            if (!await this.IfExist(dto.Name, dto.Id))
            {
                var entity = _Mapper.Map<LessonGroups>(dto);
                _dataWrapper.LessonGroupsRepository.Update(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseLessonGroupsDto>(entity),
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
        #region delete
        public async Task<ResponseResult> SoftDelete(int id)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItemAsNoTracking(filter: z => z.Id == id);
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
                _dataWrapper.LessonGroupsRepository.SoftDelete(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    Message = UI.DeleteSuccess
                };
            }
        }
        public async Task<ResponseResult> Delete(int id)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItemAsNoTracking(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: true);
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
                _dataWrapper.LessonGroupsRepository.Delete(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    Message = UI.DeleteSuccess
                };
            }
        }
        #endregion
        #region Helpers
        private async Task<bool> IfExist(string name, int? id = 0)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItemAsNoTracking(z => (id == 0 || z.Id != id) && z.Name == name);
            return entity != null ? true : false;
        }
        public async Task<List<ResponseClassDto>> ClassesList()
        {
            var list = await _dataWrapper.ClassRepository.GetItemsAsNoTracking();
            return _Mapper.Map<List<ResponseClassDto>>(list);
        }

        public async Task<List<ResponseDayDto>> DayesList()
        {
            var list = await _dataWrapper.DayRepository.GetItemsAsNoTracking();
            return _Mapper.Map<List<ResponseDayDto>>(list);
        }
        #endregion
        #region Change Position
        public async Task<ResponseResult> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var entity = await _dataWrapper.LessonGroupsRepository.GetItem(filter: cat => cat.Id == id);
            if (entity == null)
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = false,
                    Message = UI.ErrorNotFound
                };
            int startOrderNoSelect = entity.OrderNo > dto.OrderNo ? dto.OrderNo : entity.OrderNo;
            int endOrderNoSelect = entity.OrderNo < dto.OrderNo ? dto.OrderNo : entity.OrderNo;
            List<LessonGroups> OrderedEntities = new List<LessonGroups>();
            var entities = await _dataWrapper.LessonGroupsRepository.GetItems(filter: x => x.OrderNo <= endOrderNoSelect && x.OrderNo >= startOrderNoSelect,
                sort: x => x.OrderNo);
            if (entity.OrderNo > dto.OrderNo)
            {
                if (entities.Count > 0)
                    foreach (var item in entities)
                    {
                        if (item.Id == id)
                        {
                            item.OrderNo = dto.OrderNo;
                        }
                        else
                        {
                            item.OrderNo = item.OrderNo + 1;
                        }
                        OrderedEntities.Add(item);
                    }
            }
            else if (entity.OrderNo < dto.OrderNo)
            {
                if (entities.Count > 0)
                    foreach (var item in entities)
                    {
                        if (item.Id == id)
                        {
                            item.OrderNo = dto.OrderNo;
                        }
                        else
                        {
                            item.OrderNo = item.OrderNo - 1;
                        }
                        OrderedEntities.Add(item);
                    }
            }
            if (OrderedEntities.Count > 0)
            {
                _dataWrapper.LessonGroupsRepository.UpdateRange(OrderedEntities);

                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    Message = UI.OrderSuccess
                };
            }
            return new ResponseResult()
            {
                ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                IsSucceeded = false,
                Message = UI.OrderUnSuccess
            };
        }

        #endregion
    }
}
