

using Business.Interface;
using DataAccess.Data;
using DataAccess.Repository;

namespace DataAccess.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private APIContext _context;
    public UnitOfWork(APIContext context){
        _context = context;
    }
    private IJourney _journey;
    private ITransport _transport;
    private IFlight _flight;
    public IJourney Journies 
    {
        get
        {
            _journey ??= new JourneyRepository(_context);
            return _journey;
        }
    }

    public ITransport Transports 
    {
        get
        {
            _transport ??= new TransportRepository(_context);
            return _transport;
        }
    }

    public IFlight Flights
    {
        get
        {
            _flight ??= new FlightRepository(_context);
            return _flight;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}