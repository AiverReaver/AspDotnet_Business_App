using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessApp.API.Data;
using BusinessApp.API.Dtos;
using BusinessApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BusinessApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBusinessRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IBusinessRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]PageParams pageParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            pageParams.UserId = currentUserId;

            var users = await _repo.GetUsers(pageParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpGet("{id}/businesses")]
        public async Task<IActionResult> GetUserBusinesses(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var businessesFromRepo = await _repo.GetUserBusinesses(id);

            var businessForReturn = _mapper.Map<IEnumerable<BusinessForListDto>>(businessesFromRepo);

            return Ok(businessForReturn);
        }

        [HttpGet("{userId}/businesses/{id}")]
        public async Task<IActionResult> GetUserBusiness(int id, int userId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var business = await _repo.GetUserBusiness(userId, id);

            var businessToReturn = _mapper.Map<BusinessForDetailedDto>(business);

            return Ok(businessToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {   
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed to save");
        }
    }
}