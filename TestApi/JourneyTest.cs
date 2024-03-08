using API.Controllers;
using API.Services;
using AutoMapper;
using Business.Interface;

namespace TestApi;

public class JourneyTest
{
    private readonly JourneyController _controller;
    private readonly IAPIGetter _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public JourneyTest(IUnitOfWork unitOfWork, IAPIGetter service, IMapper mapper ){
        _service = service;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _controller = new JourneyController(_unitOfWork, _service, _mapper);

    }
    [Fact]
    public void Test1()
    {

    }
}