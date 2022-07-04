using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extentions;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    private readonly IPhotoService _photoService;
    public UserController(IUserRepository userRepo, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepo;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("/api/list")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
        
        return Ok(_mapper.Map<IEnumerable<MemberDTO>>(users));
    }
    
     [HttpGet("/api/{username}")]
     public async Task<ActionResult<MemberDTO>> GetUser(string username){
         
         return await _userRepository.GetMemberByUsernameAsync(username);;
         
     }

     [HttpPut("/api/update")]
     public async Task<ActionResult> UpdateInfo(MemberUpdateDTO updateObj)
     {
         var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

         _mapper.Map(updateObj,user);
         
         _userRepository.Update(user);

         if (await _userRepository.SaveAllAsync()) return NoContent();

         return BadRequest("Failed to update user");
     }


     [HttpPost("/api/add-photo")]
     public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
     {
         var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

         var result = await _photoService.AddPhotoAsync(file);

         if (result.Error != null) return BadRequest(result.Error.Message);

         var photo = new Photo
         {
             Url = result.SecureUrl.AbsoluteUri,
             Publicid = result.PublicId
         };

         if (user.Photos.Count == 0)
         {
             photo.isMain = true;
         }
         
         user.Photos.Add(photo);

         if (await _userRepository.SaveAllAsync())
         {
             return _mapper.Map<Photo, PhotoDTO>(photo);
         }

         return BadRequest("Problem adding photo");
     }
}