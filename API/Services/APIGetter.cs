
using System.Text.Json;
using API.Dtos;
using API.Dtos.Body;
using API.Dtos.Entry;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
namespace API.Services;
public class APIGetter : IAPIGetter
{
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    protected const string API_URL = "https://bitecingcom.ipage.com/testapi/avanzado.js";
    public APIGetter(IMapper mapper, IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _cache = memoryCache;
    }
    public async Task<IEnumerable<FlightDto>> GetOrSetCacheApi()
    {
        if (!_cache.TryGetValue("api", out List<FlightDto> flights))
        {
            _cache.Set("api", await GetFlights(), TimeSpan.FromMinutes(10));
            return (IEnumerable<FlightDto>)_cache.Get("api");
        }
        return flights;
    }
    private async Task<IEnumerable<FlightDto>> GetFlights()
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

    public async Task<JourneyDto> GetJourney(JourneyBodyDto entity)
    {
        IEnumerable<FlightDto> flights = await GetOrSetCacheApi();
        if (!flights.Any(e => e.Destination == entity.Destination || e.Origin == entity.Origin))
        {
            throw new ArgumentNullException("No se encontro la ubicacion");
        }
        string OriginalDestination = entity.Destination;
        string RouteDestination = null;
        List<FlightDto> data = flights.Where(e => e.Destination == entity.Destination && e.Origin == entity.Origin).ToList();
        JourneyDto journey = new();
        journey.Origin = entity.Origin;
        journey.Destination = entity.Destination;
        if (data.Any())
        {
            journey.Price = data.First().Price;
            journey.Flights = data;
            return journey;
        }
        else
        {
            List<FlightDto> flights1 = GetMostAppropiateRoute(entity, journey, RouteDestination, flights);

            journey.Flights = flights1;
            return journey;
        }
    }
    private List<FlightDto> GetMostAppropiateRoute(JourneyBodyDto entity, JourneyDto journey, string RouteDestination, IEnumerable<FlightDto> flights)
    {
        List<FlightDto> flights1 = new();
        int ident = 0;
        while (RouteDestination != entity.Destination)
        {
            FlightDto finded;
            if (ident == 0)
            {
                finded = flights.Where(e => e.Origin == entity.Origin).First();
            }
            else
            {
                finded = flights
                .Where(e => e.Origin == RouteDestination && e.Destination == entity.Destination)
                .FirstOrDefault();
                if (finded == null)
                {
                    finded = flights
                    .Where(e => e.Origin == RouteDestination)
                    .First();
                }
            }
            RouteDestination = finded.Destination;
            journey.Price += finded.Price;
            flights1.Add(finded);
            ident++;
            flights = flights.Where(e => e.Origin + e.Destination != finded.Origin + finded.Destination);
            if (RouteDestination != entity.Destination && !flights.Any(e => e.Origin == RouteDestination))
            {
                throw new Exception("No Se puede Realizar dicho viaje");
            }
        }
        return flights1;
    }
}