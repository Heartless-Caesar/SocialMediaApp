using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BugController : BaseApiController
{

    private readonly AppContextDb _context;
    
    public BugController(AppContextDb context)
    {
        _context = context;
    }

    [HttpGet("/api/auth")]
    [Authorize]
    public ActionResult<string> GetSecret()
    {
        return "Secret text";
    }

    [HttpGet("/api/not_found")]
    public ActionResult<AppUser> Not_Found()
    {
        var test = _context.Users.Find(-1);

        if (test == null) return NotFound();
        
        return Ok(test);
    }

    [HttpGet("/api/server_error")]

    public ActionResult<string> Server_Error()
    {
        var test = _context.Users.Find(-1);

        var toReturn = test.ToString();

        return toReturn;
    }

    [HttpGet("/api/bad_request")]
    public ActionResult<string> Bad_request()
    {
        return BadRequest("Not a good request");
    }
}