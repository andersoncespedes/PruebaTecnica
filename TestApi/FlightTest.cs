using API.Controllers;

namespace TestApi;
public class FlightTest : BaseTest
{
    private readonly FlightController _controller;
    public FlightTest(){
        _controller = new FlightController(_service);
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