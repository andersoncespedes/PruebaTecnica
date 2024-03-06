
namespace API.Dtos.Entry;
public class FlightDto
{
    public string Origin {get; set;}
    public string Destination {get; set;}
    public double Price {get; set;}
    public TransportDto Transport {get; set;}
}