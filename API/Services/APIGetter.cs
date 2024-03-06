using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using API.Dtos.Entry;
using AutoMapper;
using Business.Entity;
using Business.Interface;

namespace API.Services;
public class APIGetter : IAPIGetter
{
    private readonly IMapper _mapper;
    protected const string API_URL = "https://bitecingcom.ipage.com/testapi/avanzado.js";
    public APIGetter(IMapper mapper){
        _mapper = mapper;
    }
    public async Task<IEnumerable<FlightDto>> GetFlights()
    {
        HttpClient client = new();
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
        };
        HttpResponseMessage resp = await client.GetAsync(API_URL);
        string body = await resp.Content.ReadAsStringAsync();
        List<FlightsApiDto> flights = JsonSerializer.Deserialize<List<FlightsApiDto>>(body, options);
        List<FlightDto> datos = _mapper.Map<List<FlightDto>>(flights);
        return datos;
    }
}