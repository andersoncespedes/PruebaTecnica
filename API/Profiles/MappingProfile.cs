
using API.Dtos;
using API.Dtos.Entry;
using AutoMapper;
using Business.Entity;

namespace API.Profiles;
// Mapeo de los diferentes dto con sus respectivas entidades
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo del dto con su respectiva entidad
        CreateMap<FlightDto, Flight>().ReverseMap();
        CreateMap<TransportDto, Transport>().ReverseMap();
        CreateMap<JourneyDto, Journey>().ReverseMap();
        // Mapeo del dto de flight con el dto usado para deserializar la api
        CreateMap<FlightDto, FlightsApiDto>()
        .ForMember(opt => opt.DepartureStation, dest => dest.MapFrom(op => op.Origin))
        .ForMember(opt => opt.ArrivalStation, dest => dest.MapFrom(op => op.Destination))
        .ForMember(opt => opt.FlightNumber, dest => dest.MapFrom(op => op.Transport.FlightNumber))
        .ForMember(opt => opt.FlightCarrier, dest => dest.MapFrom(op => op.Transport.FlightCarrier))
        .ReverseMap();
        
    }
}
