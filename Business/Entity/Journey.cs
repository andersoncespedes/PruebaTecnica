using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Entity;
public class Journey : BaseEntity
{
    public string Origin {get; set;}
    public string Destination {get; set;}
    public double Price {get; set;}
    public ICollection<Flight> Flights {get; set;}
    public ICollection<JourneyFlight> JourneyFlights {get; set;}

}