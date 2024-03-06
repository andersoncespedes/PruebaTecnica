
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    public const string API_URL = "https://bitecingcom.ipage.com/testapi/avanzado.js";
}