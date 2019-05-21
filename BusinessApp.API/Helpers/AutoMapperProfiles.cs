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
            CreateMap<User, UserForDetailedDto>();
            CreateMap<Business, BusinessesForDetailedDto>();
            CreateMap<Photo, PhotosForDetailedDto>();
        }
    }
}