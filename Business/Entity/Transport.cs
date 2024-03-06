using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Entity;
public class Transport : BaseEntity
{   
    public string FlightCarrier {get; set;}
    public string FlightNumber {get; set;}
    public ICollection<Flight> Flights {get; set;}
}
