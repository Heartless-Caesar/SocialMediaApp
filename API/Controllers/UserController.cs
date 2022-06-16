using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppContextDb _context;

    public UserController(AppContextDb context)
    {
        _context = context;
    }

    [HttpGet("/api/list")]
    public async Task<ActionResult<List<AppUser>>> GetUsers()
    {
        var userList = await _context.Users.ToListAsync();
        
        return userList;
    }
}