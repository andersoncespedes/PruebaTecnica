

namespace API.Dtos.Entry;
public class JourneyDto
{
    public string Origin {get; set;}
    public string Destination {get; set;}
    public double Price {get; set;}
    public ICollection<FlightDto> Flights {get; set;}
}