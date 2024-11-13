using AutoMapper;
using AzureProject.Entity.Concrete;
using AzureProject.Entity.DTOs;
namespace AzureProject.Entity.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>();
            CreateMap<Slider, SliderReturnDto>();
        }
    }
}

