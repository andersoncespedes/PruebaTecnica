
using System.Text.Json;
using API.Dtos;
using API.Dtos.Body;
using API.Dtos.Entry;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
namespace API.Services;
public class APIGetter : IAPIGetter
{
    // Inicializacion de variables usadas en la clase
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    protected const string API_URL = "https://bitecingcom.ipage.com/testapi/avanzado.js";
    public APIGetter(IMapper mapper, IMemoryCache memoryCache)
    {
        // Asignacion de valor por medio de servicios previamente registrados
        _mapper = mapper;
        _cache = memoryCache;
    }
    public async Task<IEnumerable<FlightDto>> GetOrSetCacheApi()
    {
        // verificamos si no existe una key en el cache con el nombre "api"
        if (!_cache.TryGetValue("api", out List<FlightDto> flights))
        {
            // si no existe entonces la creamos asignandole el valor usando la funcion GetFlights()
            // ademas le asignamos 10 minutos para vencerse
            _cache.Set("api", await GetFlights(), TimeSpan.FromMinutes(10));
            // retornamos el valor del "api" en el cache
            return (IEnumerable<FlightDto>)_cache.Get("api");
        }
        // si existe entonces retornamos el out value
        return flights;
    }
    private async Task<IEnumerable<FlightDto>> GetFlights()
    {
        // creamos una instancia del objeto HttpClient
        HttpClient client = new();
        // creamos una opcion para que permita las traillingCommas en caso de que existan en la api llamada
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
        };
        // Obtenemos la api
        HttpResponseMessage resp = await client.GetAsync(API_URL);
        // formateamos la respuesta como string
        string body = await resp.Content.ReadAsStringAsync();
        // lo deserealizamos como una lista del dto FlightsApiDto, junto con las configuraciones
        List<FlightsApiDto> flights = JsonSerializer.Deserialize<List<FlightsApiDto>>(body, options);
        // mapeamos el resultado como nos pedia en el ejercicio usando el AutoMapper y el dto FlightDto
        List<FlightDto> datos = _mapper.Map<List<FlightDto>>(flights);
        // retornamos la lista llamada y mapeada
        return datos;
    }

    public async Task<JourneyDto> GetJourney(JourneyBodyDto entity)
    {
        // Obtencion de los vuelos de parte de la api
        IEnumerable<FlightDto> flights = await GetOrSetCacheApi();
        //verificamos si existe en la api una destinacion o un origen con el nombre pasado por parametro
        if (flights.Any(e => e.Destination != entity.Destination || e.Origin != entity.Origin))
        {
            // Si no existe entonces arroja un error 
            throw new ArgumentNullException("No se encontro la ubicacion");
        }
        // guardamos en una variable el destino
        string OriginalDestination = entity.Destination;
        // guardamos como null el destino actual
        string RouteDestination = null;
        // buscamos si existe un vuelo que coincida con esa ruta
        List<FlightDto> data = flights.Where(e => e.Destination == entity.Destination && e.Origin == entity.Origin).ToList();
        // creamos una instacia del dto journey donde guardaremos toda la informacion
        JourneyDto journey = new();
        // guadamos en la instancia el origen pasado por parametro
        journey.Origin = entity.Origin;
        // guadamos en la instancia el destino pasado por parametro
        journey.Destination = entity.Destination;
        // verificamos si existe alguna ruta en la api
        if (data.Any())
        {
            // si existe entonces se llenan los parametros
            journey.Price = data.First().Price;
            journey.Flights = data;
            // se retorna el viaje
            return journey;
        }
        else
        {
            // si no existe llamamos al metodo 
            // privado GetMostAppropiateRoute donde 
            // hara un recorrido para verificar si existe una ruta y cual seria
            // y la guardamos en la variable flights1
            List<FlightDto> flights1 = GetMostAppropiateRoute(entity, journey, RouteDestination, flights);
            // guardamos los vuelos en el objeto journey
            journey.Flights = flights1;
            // retornamos el dto
            return journey;
        }
    }
    private List<FlightDto> GetMostAppropiateRoute(JourneyBodyDto entity, JourneyDto journey, string RouteDestination, IEnumerable<FlightDto> flights)
    {
        // Creamos una lista del dto de flights
        List<FlightDto> flights1 = new();
        // creamos el contador
        int ident = 0;
        // creamos un bucle que se va a realizar hasta que la ruta actual sea igual a la ruta verdadera
        while (RouteDestination != entity.Destination)
        {
            // Inicializo una variable
            FlightDto finded;
            if (ident == 0)
            {
                // si el contador es igual a cero entonce se busca donde origen sea igual al origen pasado por parametro
                finded = flights.Where(e => e.Origin == entity.Origin).First();
            }
            else
            {
                // si el contador es mayor a 1 entonces se busca donde el origen sea igual al destino anterior
                // ademas de que el destino sea igual al pasado por parametro
                finded = flights
                .Where(e => e.Origin == RouteDestination && e.Destination == entity.Destination)
                .FirstOrDefault();
                if (finded == null)
                {
                    // sino existe entonces buscamos solamente donde el origen sea igual 
                    // a la ruta actual anterior
                    finded = flights
                    .Where(e => e.Origin == RouteDestination)
                    .First();
                }
            }
            // actualizamos la ruta actual
            RouteDestination = finded.Destination;
            // sumamos el precio total con el precio del vuelo actual
            journey.Price += finded.Price;
            // añadimos el vuelo actual a la lista de vuelos
            flights1.Add(finded);
            // sumamos el contador
            ident++;
            // actualizamos la lista pasada por api
            flights = flights.Where(e => e.Origin + e.Destination != finded.Origin + finded.Destination);
            // verificamos si no existe un origen que coincida con el destino
            if (RouteDestination != entity.Destination && !flights.Any(e => e.Origin == RouteDestination))
            {
                // si pasa esto entonces arroja una excepcion
                throw new Exception("No Se puede Realizar dicho viaje");
            }
        }
        // se retorna la lista de vuelos
        return flights1;
    }
}