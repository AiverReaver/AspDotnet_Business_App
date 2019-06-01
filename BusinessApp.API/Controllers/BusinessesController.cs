using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessApp.API.Data;
using BusinessApp.API.Dtos;
using BusinessApp.API.Helpers;
using BusinessApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessRepository _repo;
        private readonly IMapper _mapper;

        public BusinessesController(IBusinessRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBusinesses([FromQuery]PageParams pageParams)
        {

            var businesses = await _repo.GetBusinesses(pageParams);

            var businessesToReturn = _mapper.Map<IEnumerable<BusinessForListDto>>(businesses);

            Response.AddPagination(businesses.CurrentPage, businesses.PageSize,
                businesses.TotalCount, businesses.TotalPages);

            return Ok(businessesToReturn);

        }

        [AllowAnonymous]
        [HttpGet("{id}", Name="GetBusiness")]
        public async Task<IActionResult> GetBusiness(int id)
        {
            var business = await _repo.GetBusiness(id);

            var businessToReturn = _mapper.Map<BusinessForDetailedDto>(business);

            return Ok(businessToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusiness(BusinessForCreationDto businessForCreationDto)
        {
            var business = _mapper.Map<Business>(businessForCreationDto);
            var userFromRepo = await _repo.GetUser(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            
            userFromRepo.Businesses.Add(business);

            if (await _repo.SaveAll())
            {
                var businessToReturn = _mapper.Map<BusinessForDetailedDto>(business);
                return CreatedAtRoute("GetBusiness", new { id = business.Id }, businessToReturn);
            }

            return BadRequest("Cloud not create business");
        }

        [HttpPost("{id}/publish")]
        public async Task<IActionResult> PublishBusiness(int id)
        {
            var businessFromRepo = await _repo.GetBusiness(id);

            if (businessFromRepo.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            if (!businessFromRepo.IsPublishable)
                return BadRequest("Please add alteast one photo to publish your business");

            businessFromRepo.IsPublished = !businessFromRepo.IsPublished;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Can't publish this business please try again later");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness(int id, BusinessForUpdateDto businessForUpdateDto)
        {   
            var businessFromRepo = await _repo.GetBusiness(id);

            if (businessFromRepo.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            _mapper.Map(businessForUpdateDto, businessFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating Business {id} failed to save");
        }
    }
}