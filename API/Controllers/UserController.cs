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
         //Gets current username due to being autorized
         var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
         
         //Adds photo to Cloudinary
         var result = await _photoService.AddPhotoAsync(file);
         
         //In case of an error while uploading to Cloudinary
         if (result.Error != null) return BadRequest(result.Error.Message);
         
         //New photo's attributes based on the uploaded photo
         var photo = new Photo
         {
             Url = result.SecureUrl.AbsoluteUri,
             Publicid = result.PublicId
         };
         
         //Sets photo as main/profile picture if there were previously no photos
         if (user.Photos.Count == 0)
         {
             photo.isMain = true;
         }
         
         //Adds photo to the user Photo collection
         user.Photos.Add(photo);
         
         //Maps the added photo to a PhotoDTO
         if (await _userRepository.SaveAllAsync())
         {
             return _mapper.Map<PhotoDTO>(photo);
         }
         
         //Error if something goes wrong while saving changes 
         return BadRequest("Problem adding photo");
     }
}