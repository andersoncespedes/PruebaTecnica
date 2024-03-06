
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
    public JourneyController(IUnitOfWork unitOfWork, IAPIGetter api, IMapper mapper){
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _api = api;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JourneyDto>> Get([FromBody]JourneyBodyDto entity){
        Journey journey = _unitOfWork.Journies.Find(e => e.Origin == entity.Origin && e.Destination == entity.Destination).FirstOrDefault();
        if(journey != null){
            return _mapper.Map<JourneyDto>(journey);
        }else{
            JourneyDto journeydto = await _api.GetJourney(entity);
            journey = _mapper.Map<Journey>(journeydto);
            _unitOfWork.Journies.Create(journey);
            await _unitOfWork.SaveChangesAsync();
            return journeydto;
        }
    }
}
