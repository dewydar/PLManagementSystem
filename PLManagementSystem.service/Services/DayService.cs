using AutoMapper;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Core.Interfaces.IWrapper;
using PLManagementSystem.Helpers.Enum;
using PLManagementSystem.Helpers.Helpers;
using PLManagementSystem.Helpers.PassHelper;
using PLManagementSystem.Helpers.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLManagementSystem.service.Services
{
    public class DayService: IDayService
    {
        private readonly IDataWrapper _dataWrapper;
        private readonly IMapper _Mapper;
        public DayService(IDataWrapper dataWrapper, IMapper mapper)
        {
            _dataWrapper = dataWrapper;
            _Mapper = mapper;
        }
        #region Get
        public async Task<RequestDayDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.DayRepository.GetItem(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);
            return _Mapper.Map<RequestDayDto>(entity);
        }
        public async Task<ResponseDayDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.DayRepository.GetItemAsNoTracking(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);
            return _Mapper.Map<ResponseDayDto>(entity);
        }
        public async Task<List<RequestDayDto>> GetAll(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.DayRepository.GetItems(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<RequestDayDto>>(entities);
        }
        public async Task<List<ResponseDayDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.DayRepository.GetItemsAsNoTracking(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<ResponseDayDto>>(entities);
        }
        public async Task<PaginationResponseModel> GetAllPaginantion(string? name = null, bool? isActive = null, string sortDirection = "asc",
            string sortColumn = "Id", int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false)
        {
            Expression<Func<Day, object>> Sort = null;
            switch (sortColumn)
            {
                case "Id":
                    Sort = e => e.Id;
                    break;
                case "Name":
                    Sort = e => e.Name;
                    break;
                default:
                    Sort = e => e.Id;
                    break;
            }
            List<Expression<Func<Day, bool>>> search = new List<Expression<Func<Day, bool>>>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                search.Add(z => z.Name.ToLower().Contains(name.ToLower())
                || z.Name.ToLower() == name.ToLower());
            }
            PagedList<Day> PagedResult = await _dataWrapper.DayRepository.GetPagginationItems(search, Sort, sortDirection, offset, limit,
                ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);

            var result = _Mapper.Map<List<ResponseDayDto>>(PagedResult.ToList());

            if (!result.Any()) return new PaginationResponseModel(offset, null, limit, PagedResult.TotalCount, PagedResult.TotalPages);

            return new PaginationResponseModel(offset, result, limit, PagedResult.TotalCount, PagedResult.TotalPages);
        }

        #endregion
        #region Create
        public async Task<ResponseResult> Create(RequestDayDto dto)
        {
            if (!await IfExist(dto.Name, dto.Id))
            {
                Day entity = _Mapper.Map<Day>(dto);
                var MaxId = await _dataWrapper.DayRepository.GetMaxAsNoTracking(filter: z => z.Id, ignoreIsDeletedQueryFilter: true);
                entity.Id = MaxId + 1;
                entity.IsDeleted = false;
                await _dataWrapper.DayRepository.Add(entity);
                await _dataWrapper.UnitOfWork.Commit();
                ResponseResult response = new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseDayDto>(entity),
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
        public async Task<ResponseResult> Edit(RequestDayDto dto)
        {
            if (!await this.IfExist(dto.Name, dto.Id))
            {
                var entity = _Mapper.Map<Day>(dto);
                _dataWrapper.DayRepository.Update(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseDayDto>(entity),
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
            var entity = await _dataWrapper.DayRepository.GetItemAsNoTracking(filter: z => z.Id == id);
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
                _dataWrapper.DayRepository.SoftDelete(entity);
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
            var entity = await _dataWrapper.DayRepository.GetItemAsNoTracking(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: true);
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
                _dataWrapper.DayRepository.Delete(entity);
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
            var entity = await _dataWrapper.DayRepository.GetItemAsNoTracking(z => (id == 0 || z.Id != id) && z.Name == name);
            return entity != null ? true : false;
        }
        #endregion
    }
}
