using AutoMapper;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Core.Interfaces.IService;
using PLManagementSystem.Helpers.PassHelper;
using System.Net;


namespace PLManagementSystem.Service.MappProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User
            CreateMap<User, RequestUserDto>().ReverseMap();
            CreateMap<User, ResponseUserDto>()
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => WebUiUtility.Decrypt(src.Password)))
                .ReverseMap();
            #endregion
            #region Day
            CreateMap<Day, RequestDayDto>().ReverseMap();
            CreateMap<Day, ResponseDayDto>().ReverseMap();
            #endregion
            #region Class
            CreateMap<Class, RequestClassDto>().ReverseMap();
            CreateMap<Class, ResponseClassDto>().ReverseMap();
            #endregion
        }
    }
}
