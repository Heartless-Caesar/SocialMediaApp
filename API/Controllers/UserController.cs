using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UserController : BaseApiController
{
    private readonly AppContextDb _context;

    public UserController(AppContextDb context)
    {
        _context = context;
    }

    [HttpGet("/list")]
    [AllowAnonymous]
    public async Task<ActionResult<List<AppUser>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    
     [HttpGet("/api/{id}")]
     [Authorize]
     public async Task<ActionResult<AppUser>> GetUser(int id)
     {
         return await _context.Users.FindAsync(id);
     }
}