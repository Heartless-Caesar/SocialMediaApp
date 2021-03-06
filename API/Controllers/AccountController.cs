using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly AppContextDb _context;
    
    private readonly ITokenService _tokenService;
    
    public AccountController(AppContextDb context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("/api/register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO obj)
    {
        if (await UserExists(obj.Username)) return BadRequest("Username taken");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Username = obj.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(obj.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
        
        return new UserDTO
        {
            Username = obj.Username,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("/api/login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO obj)
    {
        var user = await _context.Users.SingleOrDefaultAsync(
            c => c.Username == obj.Username.ToLower());

        if (user is null) return Unauthorized("User not found");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(obj.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Passwords do not match");
            }
        }
        
        return new UserDTO
        {
            Username = obj.Username,
            Token = _tokenService.CreateToken(user)
        };
    }
    
    [HttpGet]
    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
    }
}