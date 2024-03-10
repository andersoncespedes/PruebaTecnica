
using API.Dtos.Body;
using API.Dtos.Entry;
using API.Services;
using AutoMapper;
using Business.Entity;
using Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class JourneyController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAPIGetter _api;
    private readonly IMapper _mapper;
    private readonly ILogger<JourneyController> _logger;
    public JourneyController(IUnitOfWork unitOfWork, IAPIGetter api, IMapper mapper, ILogger<JourneyController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _api = api;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JourneyDto>> Get([FromBody] JourneyBodyDto entity)
    {
        try
        {
            // Convertimos los Parametros en upper case
            entity.Origin = entity.Origin.ToUpper();
            entity.Destination = entity.Destination.ToUpper();
            // Verificamos si existe en la base de datos un viaje que coincida con el pasado por parametro
            _logger.LogInformation("Buscando Ruta en la base de datos...");
            Journey journey = _unitOfWork.Journies
                .Find(e => e.Origin == entity.Origin && e.Destination == entity.Destination)
                .FirstOrDefault();
                // verificamos si el resultado dio null
            if (journey != null)
            {
                _logger.LogInformation("Ruta Encontrada en la Base de Datos");
                // si dio diferente a null entonces mapeamos la respuesta con el JourneyDto y la retornamos 
                return _mapper.Map<JourneyDto>(journey);
            }
            else
            {
                _logger.LogWarning("Ruta No Encontrada en la base de datos");
                // si dio null entonces utilizamos el servicio api y llamamos el metodo GetJourney
                _logger.LogInformation("Obteniendo ruta por la API...");
                JourneyDto journeydto = await _api.GetJourney(entity);
                // Mapeamos el resulado como la entidad Journey
                _logger.LogInformation("Mapeando Resultados...");
                journey = _mapper.Map<Journey>(journeydto);
                // Creamos un nuevo viaje en la base de datos
                _logger.LogInformation("Guardando Resultados...");
                _unitOfWork.Journies.Create(journey);
                // Guardamos los cambios
                await _unitOfWork.SaveChangesAsync();
                // retornamos la respuesta pasada por el servicio
                return journeydto;
            }
        }
        // Control de excepciones
        catch (InvalidOperationException err)
        {
            _logger.LogError("NO SE ENCONTRO LA RUTA");
            return BadRequest(err.Message);
        }
        catch (ArgumentNullException)
        {
            _logger.LogError("NO SE ENCONTRO DESTINO O ORIGEN");
            return NotFound("No se encontro la ubicacion");
        }
        catch (Exception)
        {
            _logger.LogError("ERROR DESCONOCIDO");
            return Ok("No Se pudo realizar la operacion");
        }
    }
}
