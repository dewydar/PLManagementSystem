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

namespace PLManagementSystem.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IDataWrapper _dataWrapper;
        private readonly IMapper _Mapper;
        public UserService(IDataWrapper dataWrapper, IMapper mapper)
        {
            _dataWrapper = dataWrapper;
            _Mapper = mapper;
        }
        #region Get
        public async Task<RequestUserDto> GetById(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.UserRepository.GetItem(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);
            return _Mapper.Map<RequestUserDto>(entity);
        }
        public async Task<ResponseUserDto> GetByIdAsNoTracking(int id, bool ignoreIsDeletedQueryFilter = false)
        {
            var entity = await _dataWrapper.UserRepository.GetItemAsNoTracking(filter: z => z.Id == id, ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);
            return _Mapper.Map<ResponseUserDto>(entity);
        }
        public async Task<List<RequestUserDto>> GetAll(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.UserRepository.GetItems(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<RequestUserDto>>(entities);
        }
        public async Task<List<ResponseUserDto>> GetAllAsNoTracking(bool ignoreIsDeletedQueryFilter = false)
        {
            var entities = await _dataWrapper.UserRepository.GetItemsAsNoTracking(ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter,
                sort: z => z.Id, sortDirection: "asc");
            return _Mapper.Map<List<ResponseUserDto>>(entities);
        }
        public async Task<PaginationResponseModel> GetAllPaginantion(string? name = null,bool? isActive=null, string sortDirection = "asc",
            string sortColumn = "Id",
            int offset = 1, int limit = 10, bool ignoreIsDeletedQueryFilter = false)
        {
            Expression<Func<User, object>> Sort = null;
            switch (sortColumn)
            {
                case "Id":
                    Sort = e => e.Id;
                    break;
                case "Name":
                    Sort = e => e.Name;
                    break;
                case "IsActive":
                    Sort = e => e.IsActive;
                    break;
                case "UserName":
                    Sort = e => e.UserName;
                    break;
                default:
                    Sort = e => e.Id;
                    break;
            }
            List<Expression<Func<User, bool>>> search = new List<Expression<Func<User, bool>>>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                search.Add(z => z.Name.ToLower().Contains(name.ToLower())
                || z.Name.ToLower() == name.ToLower());
            }
            if (isActive!=null)
            {
                search.Add(z => z.IsActive== isActive);
            }
            PagedList<User> PagedResult = await _dataWrapper.UserRepository.GetPagginationItems(search, Sort, sortDirection, offset, limit,
                ignoreIsDeletedQueryFilter: ignoreIsDeletedQueryFilter);

            var result = _Mapper.Map<List<ResponseUserDto>>(PagedResult.ToList());

            if (!result.Any()) return new PaginationResponseModel(offset, null, limit, PagedResult.TotalCount, PagedResult.TotalPages);

            return new PaginationResponseModel(offset, result, limit, PagedResult.TotalCount, PagedResult.TotalPages);
        }

        #endregion
        #region Create
        public async Task<ResponseResult> Create(RequestUserDto dto)
        {
            if (!await IfExist(dto.Name, dto.Id))
            {
                User entity = _Mapper.Map<User>(dto);
                var MaxId = await _dataWrapper.UserRepository.GetMaxAsNoTracking(filter: z => z.Id, ignoreIsDeletedQueryFilter: true);
                entity.Id = MaxId + 1;
                entity.IsActive = false;
                entity.IsDeleted = false;
                entity.Password =WebUiUtility.Encrypt($"P@$$w0rd:{entity.UserName}");
                await _dataWrapper.UserRepository.Add(entity);
                await _dataWrapper.UnitOfWork.Commit();
                ResponseResult response = new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseUserDto>(entity),
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
        public async Task<ResponseResult> Edit(RequestUserDto dto)
        {
            if (!await this.IfExist(dto.Name, dto.Id))
            {
                var entity = _Mapper.Map<User>(dto);
                entity.Password = WebUiUtility.Encrypt(entity.Password);
                _dataWrapper.UserRepository.Update(entity);
                await _dataWrapper.UnitOfWork.Commit();
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.OK,
                    IsSucceeded = true,
                    ReturnData = _Mapper.Map<ResponseUserDto>(entity),
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
            var entity = await _dataWrapper.UserRepository.GetItemAsNoTracking(filter: z => z.Id == id && !z.IsAdmin);
            if (entity == null)
            {
                return new ResponseResult()
                {
                    ApiStatusCode = (int)ApiStatusCodeEnum.NotFound,
                    IsSucceeded = false,
                    Message = UI.ErrorNotFound
                };
            }
            //else if (entity != null && entity.Locations.Where(z => !z.IsDeleted).Count() > 0)
            //{
            //    return new ResponseResult()
            //    {
            //        ApiStatusCode = (int)ApiStatusCodeEnum.BadRequest,
            //        IsSucceeded = false,
            //        Message = Messages.ErrorRelatedData
            //    };
            //}
            else
            {
                _dataWrapper.UserRepository.SoftDelete(entity);
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
            var entity = await _dataWrapper.UserRepository.GetItemAsNoTracking(filter: z => z.Id == id && !z.IsAdmin, ignoreIsDeletedQueryFilter: true);
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
                _dataWrapper.UserRepository.Delete(entity);
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
            var entity = await _dataWrapper.UserRepository.GetItemAsNoTracking(z => (id == 0 || z.Id != id) && z.Name == name);
            return entity != null ? true : false;
        }
        #endregion
    }
}
