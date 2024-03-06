using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    public const string API_URL = "https://bitecingcom.ipage.com/testapi/avanzado.js";
    public Task<HttpClient> GetApi(){

    }
}