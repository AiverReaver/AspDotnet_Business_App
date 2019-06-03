using System;
using System.Linq;
using AutoMapper;
using BusinessApp.API.Dtos;
using BusinessApp.API.Models;

namespace BusinessApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.Age, options => {
                    options.MapFrom((src, dest) => src.DateOfBirth.CalculateAge());
                });
            CreateMap<Business, BusinessForListDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.ValidTill, options => {
                    options.MapFrom((src, dest) => (int)(src.User.ValidTill - DateTime.Now).TotalDays);
                });
            CreateMap<Business, BusinessForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<BusinessForUpdateDto, Business>();
            CreateMap<BusinessForCreationDto, Business>();
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Video, VideoForReturnDto>();
            CreateMap<VideoForCreationDto, Video>();
        }
    }
}