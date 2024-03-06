using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class FlightsDto
{
    public string DepartureStation {get; set;}
    public string ArrivalStation {get; set;}
    public string FlightCarrier {get; set;}
    public string FlightNumber {get; set;}
    public double Price {get; set;}
}