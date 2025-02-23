using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomFilterAttributes;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody]AddWalksDTO addWalksDTO)
        {
            
            Walk walk = mapper.Map<Walk>(addWalksDTO);
            await this.walkRepository.CreateAsync(walk);
            return Ok(this.mapper.Map<WalksDTO>(walk));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] String? filterField, [FromQuery] String? filterValue
                                                     , [FromQuery]String? sortField, [FromQuery]Boolean? isAscending
                                                     , [FromQuery]Int32 pageNumber = 1, [FromQuery]Int32 pageSize=100)
        {
            List<Walk> walks = await this.walkRepository.GetAllAsync(filterField,filterValue,sortField, pageNumber, pageSize,isAscending ?? true);
            return Ok(this.mapper.Map<List<WalksDTO>>(walks));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIDAsync([FromRoute] Guid id)
        {
            Walk? walk = await this.walkRepository.GetByIDAsync(id);
            if (walk == null) { return NotFound(); }
            return Ok(this.mapper.Map<WalksDTO>(walk));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateByIDAsync([FromRoute] Guid id, [FromBody] AddWalksDTO addWalksDTO)
        {
            Walk updatedWalk = this.mapper.Map<Walk>(addWalksDTO);
            updatedWalk = await this.walkRepository.UpdateByIDAsync(id, updatedWalk);
            if (updatedWalk==null)
            {
                return NotFound();
            }
            return Ok(this.mapper.Map<WalksDTO>(updatedWalk));
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid id)
        {
            Walk? walk = await this.walkRepository.DeleteAsync(id);
            if (walk == null) { return NotFound(); }
            return Ok(this.mapper.Map<WalksDTO>(walk));
        }
    }
}
