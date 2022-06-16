using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController
{
    private readonly AppContextDb _context;

    public UserController(AppContextDb context)
    {
        _context = context;
    }
    
    [HttpGet("/api/list")]
    
}