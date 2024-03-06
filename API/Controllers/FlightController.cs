
using API.Dtos.Entry;
using API.Services;
using Business.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class FlightController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<FlightDto>> Get(IAPIGetter api){
        return await api.GetFlights();
    }

}