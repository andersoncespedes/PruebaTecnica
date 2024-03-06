

using Business.Entity;
using Business.Interface;
using DataAccess.Data;

namespace DataAccess.Repository;
public class TransportRepository : GenericRepository<Transport>, ITransport
{
    public TransportRepository(APIContext context) : base(context)
    {
    }
}