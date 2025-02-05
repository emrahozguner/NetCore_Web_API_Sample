using API.Domain.Models;
using API.Resources;
using AutoMapper;

namespace API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();

            CreateMap<SaveProductResource, Product>()
                .ForMember(src => src.UnitOfMeasurement,
                    opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));
        }
    }
}