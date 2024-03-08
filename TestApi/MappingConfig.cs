using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Dtos.Entry;
using AutoMapper;
using Business.Entity;

namespace TestApi;
public static class MappingConfig
{
    public static MapperConfiguration ConfigurationMapper()
    {
        var conf = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FlightDto, Flight>().ReverseMap();
            cfg.CreateMap<TransportDto, Transport>().ReverseMap();
            cfg.CreateMap<FlightDto, FlightsApiDto>()
            .ForMember(opt => opt.DepartureStation, dest => dest.MapFrom(op => op.Origin))
            .ForMember(opt => opt.ArrivalStation, dest => dest.MapFrom(op => op.Destination))
            .ForMember(opt => opt.FlightNumber, dest => dest.MapFrom(op => op.Transport.FlightNumber))
            .ForMember(opt => opt.FlightCarrier, dest => dest.MapFrom(op => op.Transport.FlightCarrier))
            .ReverseMap();
            cfg.CreateMap<JourneyDto, Journey>().ReverseMap();
        });
        return conf;
    }
}
