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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusiness(int id)
        {
            var business = await _repo.GetBusiness(id);

            var businessToReturn = _mapper.Map<BusinessForDetailedDto>(business);

            return Ok(businessToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, BusinessForUpdateDto businessForUpdateDto)
        {   
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var businessFromRepo = await _repo.GetBusiness(id);

            _mapper.Map(businessForUpdateDto, businessFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating Business {id} failed to save");
        }
    }
}