using AutoMapper;
using PLManagementSystem.Core.Dtos.Request;
using PLManagementSystem.Core.Dtos.Response;
using PLManagementSystem.Core.Entities;
using PLManagementSystem.Helpers.PassHelper;

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
            #region Lesson Groups
            CreateMap<LessonGroups, RequestLessonGroupsDto>().ReverseMap();
            CreateMap<ResponseLessonGroupsDto, LessonGroups>().ReverseMap()
                .ForMember(dest => dest.ClassName, opts => opts.MapFrom(src => src.Classes.Name))
                .ReverseMap();
            #endregion
            #region Lesson Groups Days
            CreateMap<LessonGroupsDays, RequestLessonGroupsDaysDto>().ReverseMap();
            CreateMap<ResponseLessonGroupsDaysDto, LessonGroupsDays>().ReverseMap()
                .ForMember(dest => dest.DayName, opts => opts.MapFrom(src => src.Dayes.Name))
                .ForMember(dest => dest.LessonGroupName, opts => opts.MapFrom(src => src.LessonGroup.Name))
                .ReverseMap();
            #endregion
        }
    }
}
