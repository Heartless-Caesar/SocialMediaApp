using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BaseApiController : ControllerBase
{ 
    public HttpResponseMessage Options()
    {
      return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
    }
}