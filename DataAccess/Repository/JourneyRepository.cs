using System.Linq.Expressions;
using Business.Entity;
using Business.Interface;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;
public class JourneyRepository : GenericRepository<Journey>, IJourney
{
    public JourneyRepository(APIContext context) : base(context)
    {
    }
    public override IEnumerable<Journey> Find(Expression<Func<Journey, bool>> expression)
    {
        return _context.Journies.Where(expression).Include(e => e.Flights);
    }
}