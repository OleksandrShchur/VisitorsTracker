using AutoMapper;
using System;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Db.Entities;
using VisitorsTracker.ViewModels;

namespace VisitorsTracker.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region USER MAPPING

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.RefreshTokens, opts => opts.MapFrom(src => src.RefreshTokens));

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Photo, opts => opts.MapFrom(src => src.PhotoUrl))
                .ForMember(dest => dest.RoleId, opts => opts.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.RefreshTokens, opts => opts.MapFrom(src => src.RefreshTokens))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserDTO, UserInfoViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name 
                    ?? src.Email.Substring(0, src.Email.IndexOf("@", StringComparison.Ordinal))))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(
                    dest => dest.PhotoUrl,
                    opts => opts.MapFrom(src => src.PhotoUrl))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<UserViewModel, UserDTO>();

            #endregion
        }
    }
}
