using System.Diagnostics;
using API.Controllers;
using API.Dtos.Body;
using API.Dtos.Entry;
using API.Services;
using AutoMapper;
using DataAccess.Data;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace TestApi;

public class JourneyTest
{
    private readonly JourneyController _controller;
    private readonly APIGetter _service;
    private readonly UnitOfWork _unitOfWork;
    private readonly Mapper _mapper;
    private readonly APIContext _context;
    private readonly MemoryCache _cache;
    public JourneyTest()
    {
        var options = new DbContextOptionsBuilder<APIContext>()
            .UseSqlServer("Server=Anderson-pc\\ANDERSON;Database=ColombiaTravel;User Id=Anderson\\sa;Password=123456;Trust Server Certificate=True;Trusted_Connection=True;")
            .Options;
        _cache = new MemoryCache(new MemoryCacheOptions());
        _context = new APIContext(options);
        _mapper = new Mapper(MappingConfig.ConfigurationMapper());
        _service = new APIGetter(_mapper, _cache);
        _unitOfWork = new UnitOfWork(_context);
        _controller = new JourneyController(_unitOfWork, _service, _mapper);

    }
    [Fact]
    public async void GetData()
    {
        //arrange 
        JourneyBodyDto journey = new();
        journey.Destination = "bta";
        journey.Origin = "bga";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.NotNull(result.Value);
        Assert.IsType<JourneyDto>(result.Value);
        Assert.IsAssignableFrom<JourneyDto>(result.Value);
    }
    [Fact]
    public async void GetNotFound()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "xx";
        journey.Origin = "aa";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
    [Fact]
    public async void GetNoRoute()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "med";
        journey.Origin = "ctg";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
    [Fact]
    public async void GetCountFlight()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "med";
        journey.Origin = "med";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.True(result.Value?.Flights.Count < 4);
    }
    [Fact]
    public async void LessThan500ms()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "med";
        journey.Origin = "med";

        var totalTime = 0L;
        var iterations = 10; // Número de iteraciones para calcular el promedio

        for (int i = 0; i < iterations; i++)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await _controller.Get(journey);
            stopwatch.Stop();
            totalTime += stopwatch.ElapsedMilliseconds;
        }

        var averageTime = totalTime / iterations;

        Assert.True(averageTime < 500);
    }
}