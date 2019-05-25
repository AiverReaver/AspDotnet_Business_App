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
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photo.Url);
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photo.Url);
                })
                .ForMember(dest => dest.Age, options => {
                    options.MapFrom((src, dest) => src.DateOfBirth.CalculateAge());
                });
            CreateMap<Business, BusinessForListDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });
            CreateMap<Business, BusinessForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, options => {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<ProfilePhoto, ProfilePhotoForDetailedDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Video, VideoForReturnDto>();
            CreateMap<VideoForCreationDto, Video>();
        }
    }
}