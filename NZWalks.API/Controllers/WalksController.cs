using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddWalksDTO addWalksDTO)
        {
            Walk walk = mapper.Map<Walk>(addWalksDTO);
            await this.walkRepository.CreateAsync(walk);
            return Ok(this.mapper.Map<WalksDTO>(walk));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Walk> walks = await this.walkRepository.GetAllAsync();
            return Ok(this.mapper.Map<List<WalksDTO>>(walks));
        }
    }
}
