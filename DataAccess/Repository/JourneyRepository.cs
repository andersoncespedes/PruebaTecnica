using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Entity;
using Business.Interface;
using DataAccess.Data;

namespace DataAccess.Repository;
public class JourneyRepository : GenericRepository<Journey>, IJourney
{
    public JourneyRepository(APIContext context) : base(context)
    {
    }
}