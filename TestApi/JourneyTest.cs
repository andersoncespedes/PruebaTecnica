using System.Diagnostics;
using API.Controllers;
using API.Dtos.Body;
using API.Dtos.Entry;
using Microsoft.AspNetCore.Mvc;

namespace TestApi;

public class JourneyTest : BaseTest
{
    private readonly JourneyController _controller;

    public JourneyTest()
    {
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
    public async void LessThan500msResponse()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "med";
        journey.Origin = "med";

        var totalTime = 0L;
        var iterations = 10;

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