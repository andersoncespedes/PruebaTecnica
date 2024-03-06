using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Interface;
public interface IUnitOfWork
{
    IJourney Journies {get;}
    ITransport Transports {get;}
    IFlight Flights {get;}
    Task<int> SaveChangesAsync();
}