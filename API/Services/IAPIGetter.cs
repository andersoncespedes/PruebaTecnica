
using API.Dtos.Body;
using API.Dtos.Entry;


namespace API.Services;
public interface IAPIGetter
{
    Task<IEnumerable<FlightDto>> GetFlights();
    Task<JourneyDto> GetJourney(JourneyBodyDto entity);
}