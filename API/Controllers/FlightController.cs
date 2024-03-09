
using API.Dtos.Entry;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class FlightController : BaseApiController
{
    private readonly IAPIGetter _api;
    public FlightController(IAPIGetter api){
        _api = api;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<FlightDto>> Get(){
        return await _api.GetOrSetCacheApi();
    }

}