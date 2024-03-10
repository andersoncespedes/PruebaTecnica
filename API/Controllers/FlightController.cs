
using API.Dtos.Entry;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class FlightController : BaseApiController
{
    private readonly IAPIGetter _api;
    private readonly ILogger<FlightController> _logger;
    public FlightController(IAPIGetter api, ILogger<FlightController> logger){
        _api = api;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<FlightDto>> Get(){
        _logger.LogInformation("Obteniendo los vuelos....");
        IEnumerable<FlightDto> api = await _api.GetOrSetCacheApi();
        _logger.LogInformation("Vuelos Encontrados");
        return api;
    }

}