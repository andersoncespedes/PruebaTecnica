using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using AutoMapper;
using DataAccess.Data;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace TestApi;
public class BaseTest
{
    protected readonly APIGetter _service;
    protected readonly UnitOfWork _unitOfWork;
    protected readonly Mapper _mapper;
    protected readonly APIContext _context;
    protected readonly MemoryCache _cache;
    public BaseTest()
    {
        var options = new DbContextOptionsBuilder<APIContext>()
         .UseSqlServer("Server=Anderson-pc\\ANDERSON;Database=ColombiaTravel;User Id=Anderson\\sa;Password=123456;Trust Server Certificate=True;Trusted_Connection=True;")
         .Options;
        _cache = new MemoryCache(new MemoryCacheOptions());
        _context = new APIContext(options);
        _mapper = new Mapper(MappingConfig.ConfigurationMapper());
        _service = new APIGetter(_mapper, _cache);
        _unitOfWork = new UnitOfWork(_context);
    }
}