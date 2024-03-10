using System.Diagnostics;
using API.Controllers;
using API.Dtos.Body;
using API.Dtos.Entry;
using Microsoft.AspNetCore.Mvc;

namespace TestApi;

public class JourneyTest : BaseTest<JourneyController> 
{
    // Inicializacion del controlador de Journey
    private readonly JourneyController _controller;

    public JourneyTest()
    {
        // Instanciacion del controlador de Journey
        _controller = new JourneyController(_unitOfWork, _service, _mapper, _logger);

    }
    [Fact]
    // Prueba para verificar si la informacion responde correctamente
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
    // Prueba para verificar si lanza la respuesta notfound 404

    public async void GetNotFound()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "xx";
        journey.Origin = "a";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
    [Fact]
    // Verificacion si la cantidad de vuelos es menor de lo esperada
    public async void GetCountFlight()
    {
        JourneyBodyDto journey = new();
        journey.Destination = "med";
        journey.Origin = "cuc";
        //act 
        var result = await _controller.Get(journey);
        //assert
        Assert.True(result.Value?.Flights.Count < 4);
    }
    [Fact]
    // Verificacion de rendimiento de respuesta de la api
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