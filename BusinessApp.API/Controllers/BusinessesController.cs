using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessApp.API.Data;
using BusinessApp.API.Dtos;
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
        public async Task<IActionResult> GetBusinesses()
        {
            var businesses = await _repo.GetBusinesses();

            var businessesToReturn = _mapper.Map<IEnumerable<BusinessForListDto>>(businesses);

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
    }
}