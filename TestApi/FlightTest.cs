using API.Controllers;

namespace TestApi;
public class FlightTest : BaseTest<FlightController>
{
    private readonly FlightController _controller;
    public FlightTest(){
        _controller = new FlightController(_service, _logger);
    }
    [Fact]
    // Prueba Para verifcar la llegada correcta de data
    public async void GetData()
    {
        //act 
        var result = await _controller.Get();
        //assert
        Assert.NotNull(result);
    }
}