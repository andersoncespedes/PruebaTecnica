
namespace Business.Interface;
public interface IUnitOfWork
{
    IJourney Journies {get;}
    ITransport Transports {get;}
    IFlight Flights {get;}
    Task<int> SaveChangesAsync();
}