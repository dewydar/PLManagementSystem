using AutoMapper;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;


namespace PLManagementSystem.Service.MappProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, RequestUserDto>().ReverseMap();
            CreateMap<User, ResponseUserDto>().ReverseMap();
            #endregion
        }
    }
}
