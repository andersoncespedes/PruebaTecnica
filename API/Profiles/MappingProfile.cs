
using API.Dtos;
using API.Dtos.Entry;
using AutoMapper;
using Business.Entity;

namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile(){
        this.CreateMap<FlightDto, FlightsApiDto>()
        .ForMember(opt => opt.DepartureStation, dest => dest.MapFrom(op => op.Origin))
        .ForMember(opt => opt.ArrivalStation, dest => dest.MapFrom(op => op.Destination))
        .ForMember(opt => opt.FlightNumber, dest => dest.MapFrom(op => op.Transport.FlightNumber))
        .ForMember(opt => opt.FlightCarrier, dest => dest.MapFrom(op => op.Transport.FlightCarrier))
        .ReverseMap();
    }
}
