using Business.Entity;
using Business.Interface;
using DataAccess.Data;

namespace DataAccess.Repository;
public class FlightRepository : GenericRepository<Flight>, IFlight
{
    public FlightRepository(APIContext context) : base(context)
    {
    }
}