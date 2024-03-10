
using API.Services;
using AutoMapper;
using DataAccess.Data;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace TestApi;
// Base Para las pruebas
public class BaseTest<T> where T : class
{
    protected readonly APIGetter _service;
    protected readonly UnitOfWork _unitOfWork;
    protected readonly Mapper _mapper;
    protected readonly APIContext _context;
    protected readonly MemoryCache _cache;
    protected readonly FakeLogger<T> _logger;
    public BaseTest()
    {
        // Instancias y configuracione usadas para las pruebas
        var options = new DbContextOptionsBuilder<APIContext>()
         .UseSqlServer("Server=Anderson-pc\\ANDERSON;Database=ColombiaTravel;User Id=Anderson\\sa;Password=123456;Trust Server Certificate=True;Trusted_Connection=True;")
         .Options;
        _cache = new MemoryCache(new MemoryCacheOptions());
        _context = new APIContext(options);
        _mapper = new Mapper(MappingConfig.ConfigurationMapper());
        _service = new APIGetter(_mapper, _cache);
        _unitOfWork = new UnitOfWork(_context);
        _logger = new FakeLogger<T>();
    }
}
    public class FakeLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    }