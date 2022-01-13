using AutoMapper;
using Data.Dtos;
using Entities.DataModel;

namespace FeyzaBagiroz_Odev2.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() : base()
        {
            CreateMap<ContainerDto, Container>();
            CreateMap<Container, ContainerDto>();
            CreateMap<VehicleDto, Vehicle>();
            CreateMap<Vehicle, VehicleDto>();
        }
    }
}
