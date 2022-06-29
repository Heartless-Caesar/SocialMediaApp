using API.Data;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepo)
    {
        _userRepository = userRepo;
    }

    [HttpGet("/api/list")]
    public async Task<ActionResult<List<AppUser>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }
    
     [HttpGet("/api/{id}")]
     public async Task<ActionResult<AppUser>> GetUser(int id)
     {
         return await _userRepository.GetUserByIdAsync(id);
     }
}