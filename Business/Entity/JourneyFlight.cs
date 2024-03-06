using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Entity;
public class JourneyFlight
{
    public int IdJourneyFK {get; set;}
    public Journey Journey {get; set;}
    public int IdFlightFK {get; set;}
    public Flight Flight {get; set;}
}